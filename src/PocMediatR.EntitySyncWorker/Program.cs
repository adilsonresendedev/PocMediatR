using Microsoft.EntityFrameworkCore;
using PocMediatR.Domain.Context;
using PocMediatR.EntitySyncWorker.Dispacher;
using PocMediatR.EntitySyncWorker.Dispacher.Handlers;
using PocMediatR.EntitySyncWorker.Messages;
using PocMediatR.EntitySyncWorker.Services;
using PocMediatR.EntitySyncWorker.Workers;
using PocMediatR.Infra.Context;
using PocMediatR.Infra.MessageBus;
using PocMediatR.Infra.Settings;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<MessageProcessingWorker>();
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(nameof(RabbitMqSettings)));
builder.Services.AddSingleton<IConsumerMessageBus, RabbitMqConsumerMessageBus>();
builder.Services.AddSingleton<IMessageDispacher, MessageDispatcher>();
builder.Services.AddScoped<IMessageHandler<PriceTypeMessage>, PriceTypeHandler>();
builder.Services.AddScoped<DatabaseSeedService>();

builder.Services.AddDbContext<PocMediatrReadContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSql")));

builder.Services.AddScoped<IPocMediatrReadContext, PocMediatrReadContext>();

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var seedService = scope.ServiceProvider.GetRequiredService<DatabaseSeedService>();
    await seedService.ApplyPendingScriptsAsync();
}

host.Run();
