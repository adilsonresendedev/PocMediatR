using Microsoft.EntityFrameworkCore;
using PocMediatR.EntitySyncWorker;
using PocMediatR.EntitySyncWorker.Dispacher;
using PocMediatR.EntitySyncWorker.Dispacher.Handlers;
using PocMediatR.EntitySyncWorker.Messages;
using PocMediatR.Infra.Context;
using PocMediatR.Infra.MessageBus;
using PocMediatR.Infra.Settings;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(nameof(RabbitMqSettings)));
builder.Services.AddSingleton<IConsumerMessageBus, RabbitMqConsumerMessageBus>();
builder.Services.AddSingleton<IMessageDispacher, MessageDispatcher>();
builder.Services.AddScoped<IMessageHandler<PriceTypeMessage>, PriceTypeHandler>();

builder.Services.AddDbContext<PocMediatRReadContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")));

var host = builder.Build();
host.Run();
