using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //IServiceCollection parameter to register services to the IoC container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FundooContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:FundooDB"]));
            services.AddControllers();

            //The AddTransient method registers the Interface and service with a Transient lifetime (for User)
            services.AddTransient<IUserBL, UserBL>();
            services.AddTransient<IUserRL, UserRL>();

            //The AddTransient method registers the Interface and service with a Transient lifetime (for Notes)
            services.AddTransient<INotesBL,NotesBL>();
            services.AddTransient<INotesRL, NotesRL>();

            services.AddTransient<ICollabBL, CollabBL>();
            services.AddTransient<ICollabRL, CollabRL>();

            services.AddTransient<ILabelBL, LabelBL>();
            services.AddTransient<ILabelRL, LabelRL>();

            //swagger implementation
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Fundoo Swagger UI",
                    Description = "Swagger UI for Implementation on FundooApp API",
                });

                //Swagger Authorization Implemantation 
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type=ReferenceType.SecurityScheme,
                                 Id="Bearer"
                             }
                         },
                          new string[]{}
                     }
                 });

            });
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))       //Configuration["JwtToken:SecretKey"]
                };
            });

            //Redis Server Cache Implementation
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
            //Injecting Dependency Injection Services
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Configure application request pipeline by using IApplicationBuilder
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Middlewares which has follows orders by sequence
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //CORS middleware is configured for React front End Fundoo App
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing FundooApp API V1");
            });
        }
    }
}
