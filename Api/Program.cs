using System.Text;
using Api.Config;
using Api.Service.TokenService;
using Aplicacao.Service.ArquivosStorage;
using Aplicacao.UseCase.AdminUseCase.AdminLogin;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarArquivos;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoCadastrar;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoExcluirArquivos;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListar;
using Infraestrutura.Repositorio;
using Infraestrutura.Repositorio.AdminRepositorio;
using Infraestrutura.Repositorio.ProdutoRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://0.0.0.0:7048;http://0.0.0.0:5288");

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();

    var account = new CloudinaryDotNet.Account(
        configuration["Cloudinary:CloudName"],
        configuration["Cloudinary:ApiKey"],
        configuration["Cloudinary:ApiSecret"]
    );

    return new CloudinaryDotNet.Cloudinary(account);
});

builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

builder.Services.AddScoped<IAdminRepositorio, EFCoreAdminRepositorio>();
builder.Services.AddScoped<AdminLoginUseCase>();

builder.Services.AddScoped<IProdutoRepositorio, EFCoreProdutoRepositorio>();
builder.Services.AddScoped<ProdutoCadastrarUseCase>();
builder.Services.AddScoped<ProdutoListarUseCase>();
builder.Services.AddScoped<ProdutoAdicionarArquivosUseCase>();
builder.Services.AddScoped<ProdutoExcluirArquivosUseCase>();

builder.Services.AddScoped<IArquivosStorageService, ArquivosStorageService>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
    { 
        Name = "Authorization", 
        Type = SecuritySchemeType.ApiKey, 
        Scheme = "Bearer", 
        BearerFormat = "JWT", 
        In = ParameterLocation.Header, 
        Description = "JWT Authorization header using the Bearer scheme. " +
                      "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                      "\r\n\r\nExample: \"Bearer 12345abcdef\"", 
    }); 
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    { 
        { 
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme, 
                    Id = "Bearer" 
                } 
            }, 
            new string[] {} 
        } 
    }); 
});

//Converting enums to string description
builder.Services
    .AddControllersWithViews()
    .AddNewtonsoftJson(options => 
        options
            .SerializerSettings
            .Converters
            .Add(new StringEnumConverter())
    );

builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(
        options =>
        {
            var key = Encoding.ASCII.GetBytes(JwtConfig.Secret);
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }
    );

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();