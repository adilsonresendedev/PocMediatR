using Microsoft.EntityFrameworkCore;
using PocMediatR.Domain.Entities;
using PocMediatR.Infra.Context;

namespace PocMediatR.EntitySyncWorker.Services
{
    public class DatabaseSeedService
    {
        private readonly PocMediatrReadContext _context;
        private readonly string _scriptsFolder;

        public DatabaseSeedService(PocMediatrReadContext context)
        {
            _context = context;
            _scriptsFolder = GetScriptsFolder();
        }

        public async Task ApplyPendingScriptsAsync()
        {
            await EnsureDatabaseExistsAsync(
                "Host=postgres;Port=5432;Username=postgres;Password=1234", "PocMediatrReadDb"
            );

            await SetUpDatabase();
            var scripts = await GetScripts();
            await ApplyScripts(scripts);
        }

        private async Task EnsureDatabaseExistsAsync(string connectionString, string databaseName)
        {
            var builder = new Npgsql.NpgsqlConnectionStringBuilder(connectionString)
            {
                Database = "postgres"
            };

            await using var conn = new Npgsql.NpgsqlConnection(builder.ConnectionString);
            await conn.OpenAsync();

            var existsCmd = new Npgsql.NpgsqlCommand(
                "SELECT 1 FROM pg_database WHERE datname = @name", conn);
            existsCmd.Parameters.AddWithValue("name", databaseName);

            var exists = await existsCmd.ExecuteScalarAsync();

            if (exists == null)
            {
                var createCmd = new Npgsql.NpgsqlCommand(
                    $"CREATE DATABASE \"{databaseName}\"", conn);
                await createCmd.ExecuteNonQueryAsync();
                Console.WriteLine($"Database '{databaseName}' created.");
            }
            else
            {
                Console.WriteLine($"Database '{databaseName}' already exists.");
            }
        }

        private async Task ApplyScripts(IEnumerable<string> scripts)
        {
            foreach (var script in scripts)
            {
                var sql = await File.ReadAllTextAsync(script);

                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    await _context.Database.ExecuteSqlRawAsync(sql);

                    _context.SchemaVersions.Add(new SchemaVersion
                    {
                        ScriptName = Path.GetFileName(script),
                        AppliedOn = DateTime.UtcNow
                    });

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    Console.WriteLine($"Applied script: {Path.GetFileName(script)}");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Failed script: {Path.GetFileName(script)} - {ex.Message}");
                    throw;
                }
            }
        }

        private async Task<IEnumerable<string>> GetScripts()
        {
            var appliedScripts = await _context.SchemaVersions
                            .Select(s => s.ScriptName)
                            .ToListAsync();

            Console.WriteLine($"Scripts folder: {_scriptsFolder}");

            var scripts = Directory.GetFiles(_scriptsFolder, "*.sql")
                .OrderBy(f => f)
                .Where(f => !appliedScripts.Contains(Path.GetFileName(f)));
            return scripts;
        }

        private async Task SetUpDatabase()
        {
            await _context.Database.ExecuteSqlRawAsync(@"CREATE EXTENSION IF NOT EXISTS ""pgcrypto"";");

            await _context.Database.ExecuteSqlRawAsync(@"
                CREATE TABLE IF NOT EXISTS ""SchemaVersions"" (
                    ""Id"" uuid PRIMARY KEY,
                    ""ScriptName"" TEXT NOT NULL,
                    ""AppliedOn"" TIMESTAMP NOT NULL DEFAULT NOW()
                );");
        }

        private static string GetScriptsFolder()
        {
            var defaultPath = Path.Combine(AppContext.BaseDirectory, "Persistence", "Scripts");

            if (Directory.Exists(defaultPath))
                return defaultPath;

            throw new DirectoryNotFoundException(
                $"Could not locate scripts folder at {defaultPath}");
        }
    }
}   
