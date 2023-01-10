using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NZWalks.API.Data;
using NZWalks.API.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //this is for JWT Tocken for unlock 
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "Jwt Authencation ",
        Description = "Enter Vilad Jwt Bearer Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }

    };
    options.AddSecurityDefinition(securitySchema.Reference.Id, securitySchema);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securitySchema,new string []{ } }

    });


}
);
builder.Services.AddFluentValidation(Options => Options.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddDbContext<NZWalksDbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
});
//yha pe hm IRepoisitry Enjected kr rhe hai Servises  
//I implement RegionRepositori in Controller 
builder.Services.AddScoped<IRegionRepository, RegionRepositori>();
builder.Services.AddScoped<IWalkRepositori, WalkRepositori>();
builder.Services.AddScoped<IWalkDiffucaltyRepository, WalkDiffucaltyRepository>();
builder.Services.AddScoped<ITockenHandler, TockenHandlerRepository>();

//builder.Services.AddSingleton<IUserRepository, StaticUserRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(option =>
//    option.TokenValidationParameters = new TokenValidationParameters{ 
//        ValidateIssuer= true,
//        ValidateAudience= true, 
//        ValidateLifetime= true,
//        ValidateIssuerSigningKey= true,
//        ValidAudience = builder.Configuration["Jwt:Audience"], 
//        IssuerSigningKey=new SymmetricSecurityKey(Encoding
//        .UTF8.GetBytes(builder.Configuration["Jwt: Key"]))
//     });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();




//FluentValidation
//FluentValidation.AspNetCore
//FluentValidation.DependencyInjectionExtensions


//Microsoft.AspNetCore.Authentication.JwtBearer
//Microsoft.IdentityModel.Tokens
//System.IdentityModel.Tokens.Jwt



//Install Automapper Packages
//Automapper
//Automapper.Extensions.Microsoft.DependencyInjection


//INSTALL ENTITY FRAMEWORK CORE NUGET PACKAGES
//Microsoft.Entityframeworkcore.SqlServer
//Microsoft.Entityframeworkcore.Tools