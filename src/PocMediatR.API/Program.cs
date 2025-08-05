using Microsoft.EntityFrameworkCore;
using PocMediatR.API.Middlewares;
using PocMediatR.Application.Configurations.FluentValidation;
using PocMediatR.Application.Configurations.Mediator;
using PocMediatR.Common.Translations;
using PocMediatR.Domain.Context;
using PocMediatR.Infra.Context;
using PocMediatR.Infra.MessageBus;
using PocMediatR.Infra.Settings;

var supportedCultures = new string[] { AcceptedLanguages.En_US, AcceptedLanguages.Pt_Br };
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<PocMediatRWriteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddDbContext<PocMediatRReadContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSql")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator();
builder.Services.AddFluentValidation();
builder.Services.AddScoped<IPocMediatRContext, PocMediatRWriteContext>();

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(nameof(RabbitMqSettings)));
builder.Services.AddSingleton<IPublisherMessageBus, RabbitMqPublisherMessageBus>();

var app = builder.Build();

if (app.Environment.IsDevelopment())    
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ControllerMiddleware>();

var LocalizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(AcceptedLanguages.En_US)
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

using (var scope = app.Services.CreateScope())
{
    var writeDbContext = scope.ServiceProvider.GetRequiredService<PocMediatRWriteContext>();
    writeDbContext.Database.Migrate();
}

app.UseRequestLocalization(LocalizationOptions);
app.Run();
