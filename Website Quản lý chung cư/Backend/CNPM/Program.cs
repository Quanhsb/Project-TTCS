using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CNPM.Service.Interfaces;
using CNPM.Service.Implementations;
using CNPM.Repository.Interfaces;
using CNPM.Repository.Implementations;
using System.Text;
using CNPM;
InitDatabase.ResetDb();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<INhanKhauService, NhanKhauService>();
builder.Services.AddSingleton<INhanKhauRepository, NhanKhauRepository>();
builder.Services.AddSingleton<IKhoanThuService, KhoanThuService>();
builder.Services.AddSingleton<IKhoanThuRepository, KhoanThuRepository>();
builder.Services.AddSingleton<IHoKhauService, HoKhauService>();
builder.Services.AddSingleton<IHoKhauRepository, HoKhauRepository>();
builder.Services.AddSingleton<ITamTruService, TamTruService>();
builder.Services.AddSingleton<ITamTruRepository, TamTruRepository>();
builder.Services.AddSingleton<ITamVangService, TamVangService>();
builder.Services.AddSingleton<ITamVangRepository, TamVangRepository>();
builder.Services.AddSingleton<IXeRepository, XeRepository>();
builder.Services.AddSingleton<ICanHoRepository, CanHoRepository>();
builder.Services.AddSingleton<ICanHoService, CanHoService>();
builder.Services.AddHttpClient();
var app = builder.Build();
app.UseSwagger();
app.UseAuthorization();
app.UseAuthentication();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllers();
app.UseSwaggerUI();
app.Run();