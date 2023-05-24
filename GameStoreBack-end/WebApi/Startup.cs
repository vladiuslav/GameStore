using AutoMapper;
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DLL.Data;
using DLL.Interafeces;
using DLL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi;
using GameStore.WebApi.Middleware;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using GameStore.DataLogic.Interafeces;
using GameStore.DataLogic.Repositories;
using GameStrore.BusinessLogic.Interfaces;
using GameStrore.BusinessLogic.Services;

namespace WEBAPI
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(setup =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });

            services.AddDbContext<GameStoreDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyDatabase")));

            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IPassswordWithSaltRepository, PasswordWithSaltRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICartRepository, CartRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<ISearchFilterService, SearchFilterService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IСartService, CartService>();

            services.AddAutoMapper(typeof(AutoMapperProfile),typeof(AutoMapperProfileBll));

            services.AddCors(options =>
            {
                options.AddPolicy(name: Configuration.GetValue<string>("MyAllowSpecificOrigins"),
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            services.AddControllers();


            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(Configuration.GetValue<string>("MyAllowSpecificOrigins"));

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
