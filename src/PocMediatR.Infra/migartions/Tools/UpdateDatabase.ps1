$solution = "../../../PocMediatR.API"
$project = "../../../PocMediatR.Infra/PocMediatR.Infra.csproj"
$context = "PocMediatRContext"
$migrationName = "20250703203007_InitialCreate"

dotnet ef database update $migrationName -s $solution -c $context -v