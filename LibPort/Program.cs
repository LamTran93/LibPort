
using LibPort.Contexts;
using LibPort.Models;
using LibPort.Services.Authentication;
using LibPort.Services.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibPort
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<LibraryContext>
                (opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

            builder.Services
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.Authority = builder.Configuration["Jwt:Authority"];
                    o.Audience = builder.Configuration["Jwt:Audience"];
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true
                    };
                });

            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("NormalUserOnly", p => p.RequireClaim("user_type", UserType.NormalUser.ToString()));
                opt.AddPolicy("SuperUserOnly", p => p.RequireClaim("user_type", UserType.SuperUser.ToString()));
            });

            builder.Services.AddSingleton<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
        }
    }
}
