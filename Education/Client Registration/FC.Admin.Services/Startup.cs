using System.Reflection;
using System.Text;
using FC.Admin.Services.Helpers;
using FC.Admin.Services.Services;
using FC.Extension.HTTP.APIHandler;
using FC.OAuth.Services.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace FC.OAuth.Services;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        #region Enable Serilog For MongoDB.
        var appSettingsSection = Configuration.GetSection("AppSettings");
        var appSettings = appSettingsSection.Get<AppSettings>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Console()
            .WriteTo.MongoDBBson(appSettings.LogMongo,
                collectionName: "fc.clients.log",
                cappedMaxSizeMb: 10,
                cappedMaxDocuments: 1500
            )//Enable Mongodb Logging
            .CreateLogger();
        Log.Information("Enabled Serilog Logging with Console & Mongodb");
        #endregion
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

        #region Configuration Section
        var appSettingsSection = Configuration.GetSection("AppSettings");
        var appSettings = appSettingsSection.Get<AppSettings>();
        #endregion
        
        #region JWT Configuration

        var key = Encoding.ASCII.GetBytes(appSettings.Secret);
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });


        #endregion
        
        #region For Razor pages

        services.Configure<AppSettings>(appSettingsSection);
        //Session Handling and enabling
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.HttpOnly = true;
            // Make the session cookie essential
            options.Cookie.IsEssential = true;
        });
        services.AddHttpContextAccessor();
        services.AddRazorPages().AddSessionStateTempDataProvider(); ;
        #endregion
       
        #region Enable Web API with DI
        services.AddControllers
            (
                option => option.Filters.Add(new ApiExceptionFilter())
            )
            .AddNewtonsoftJson();//Enables Web API
        services.AddHttpContextAccessor();//TO Access HTTPConetext from constructor.
        services.AddTransient(typeof(IConnectionService), typeof(ConnectionService));
        // configure DI for application services
        services.AddScoped<IUserService, UserService>();
        #endregion
        
        #region Swagger Implementation
            //Ref :https://www.c-sharpcorner.com/article/swagger-in-dotnet-core/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "POS Web API",
                    Description = "A full functioning Web API for POS.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "FC POS ",
                        Email = "fc.pos@gmail.com",
                        Url = new Uri("https://example.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under OpenApiLicense",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                }
                );
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                // Set the comments path for the Swagger JSON and UI.    
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        #region Enable Swagger in Development mode.
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fire Cloud Client Application API V1.0");
        });
        #endregion
        
        #region Enable Serilog
        app.UseSerilogRequestLogging(); //Enables Serilog
        #endregion
        
        #region Enable CORS
        // global cors policy
        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        #endregion
        
        // custom jwt auth middleware
        app.UseMiddleware<JwtMiddleware>();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        //app.UseAuthorization();
        app.UseSession();//Enable Sessions for this application.
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();//Enables Web API
            endpoints.MapRazorPages();
        });
    }
}
