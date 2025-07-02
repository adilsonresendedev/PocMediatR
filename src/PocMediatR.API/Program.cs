using Microsoft.EntityFrameworkCore;
using PocMediatR.API.Middlewares;
using PocMediatR.Application.Configurations.FluentValidation;
using PocMediatR.Application.Configurations.Mediator;
using PocMediatR.Common.Translations;
using PocMediatR.Domain.Context;
using PocMediatR.Infra.Context;

var supportedCultures = new string[] { AcceptedLanguages.En_US, AcceptedLanguages.Pt_Br };
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<PocMediatRContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator();
builder.Services.AddFluentValidation();
builder.Services.AddScoped<IPocMediatRContext, PocMediatRContext>();

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

app.UseRequestLocalization(LocalizationOptions);
app.Run();
