using System.Text;
using Api.Config;
using Api.Service.TokenService;
using Aplicacao.Service.ArquivosStorage;
using Aplicacao.Service.Email;
using Aplicacao.UseCase.AdminUseCase.AdminLogin;
using Aplicacao.UseCase.Email.EmailDuvidas;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarArquivos;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoCadastrar;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoExcluirArquivos;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListar;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListarById;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoMudarStatus;
using CloudinaryDotNet;
using Infraestrutura.Repositorio;
using Infraestrutura.Repositorio.AdminRepositorio;
using Infraestrutura.Repositorio.ProdutoRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://127.0.0.1:5001");

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();

    var account = new Account(
        configuration["Cloudinary:CloudName"],
        configuration["Cloudinary:ApiKey"],
        configuration["Cloudinary:ApiSecret"]
    );

    return new Cloudinary(account);
});

builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<EmailDuvidasUseCase>();

builder.Services.AddScoped<IAdminRepositorio, EFCoreAdminRepositorio>();
builder.Services.AddScoped<AdminLoginUseCase>();

builder.Services.AddScoped<IProdutoRepositorio, EFCoreProdutoRepositorio>();
builder.Services.AddScoped<ProdutoCadastrarUseCase>();
builder.Services.AddScoped<ProdutoListarUseCase>();
builder.Services.AddScoped<ProdutoListarByIdUseCase>();
builder.Services.AddScoped<ProdutoAdicionarArquivosUseCase>();
builder.Services.AddScoped<ProdutoExcluirArquivosUseCase>();
builder.Services.AddScoped<ProdutoMudarStatusUseCase>();

builder.Services.AddScoped<IArquivosStorageService, ArquivosStorageService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme.\r\n\r\n" +
            "Enter 'Bearer' [space] and then your token.\r\n\r\n" +
            "Example: \"Bearer 12345abcdef\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var key = Encoding.ASCII.GetBytes(JwtConfig.Secret);

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("FrontEnd", policy =>
        {
            policy
                .WithOrigins("http://localhost:3000",
                            "http://localhost:3001",
                            "https://admin-develop.sgdiscos.com.br",             
                            "https://sgdiscos.com.br",
                            "https://develop.sgdiscos.com.br")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("FrontEnd");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();