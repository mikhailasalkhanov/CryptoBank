using System.Reflection;
using CryptoBank.Database;
using CryptoBank.Features.News.Options;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();

builder.Services.AddMediatR(config => 
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<CryptobankContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CryptoBank")));

builder.Services.Configure<NewsOptions>(builder.Configuration.GetSection("NewsOptions"));

var app = builder.Build();

app.UseHttpsRedirection();
app.MapFastEndpoints();

app.Run();
