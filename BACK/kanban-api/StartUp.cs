using kanban_api.Context;
using kanban_api.Interface;
using kanban_api.Repository;
using Microsoft.AspNetCore.Builder;

namespace kanban_api
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }

        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Services
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            #region Contexts
            int CommandTimeOut = Configuration.GetValue<int>("CommandTimeOut");
            //services.AddDbContext<KanbanApiContext>(options =>
            //    options.Use .UseIn(Configuration.GetConnectionString("MIDe"),
            //    sqlServerOptions => sqlServerOptions.CommandTimeout(CommandTimeOut)));
            #endregion


            #region Interfaces/Repositories

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            

            #endregion

            #region REST Configurations

            //services.AddControllers(config =>
            //{
            //    config.Filters.Add(typeof(CustomExceptionFilter));
            //});

            //services.AddControllers().AddNewtonsoftJson(options =>
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //);

            #endregion

            //#region Authentication Configurations

            //var signingConfigurations = new SigningConfigurations();
            //services.AddSingleton(signingConfigurations);

            ////Declaração das configurações de Token lendo o AppSettings.json
            //var tokenConfigurations = new TokenConfigurations();
            //new ConfigureFromConfigurationOptions<TokenConfigurations>(
            //    Configuration.GetSection("TokenConfigurations"))
            //            .Configure(tokenConfigurations);

            //services.AddSingleton(tokenConfigurations);

            //services.AddAuthentication(authOptions =>
            //{
            //    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(bearerOptions =>
            //{
            //    var paramsValidation = bearerOptions.TokenValidationParameters;
            //    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
            //    paramsValidation.ValidAudience = tokenConfigurations.Audience;
            //    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

            //    // Valida a assinatura de um token recebido
            //    paramsValidation.ValidateIssuerSigningKey = true;

            //    // Verifica se um token recebido ainda é válido
            //    paramsValidation.ValidateLifetime = true;

            //    // Tempo de tolerância para a expiração de um token (utilizado
            //    // caso haja problemas de sincronismo de horário entre diferentes
            //    // computadores envolvidos no processo de comunicação)
            //    paramsValidation.ClockSkew = TimeSpan.Zero;
            //});

            ////Ativa o uso do token como forma de autorizar o acesso
            //// a recursos deste projeto
            //services.AddAuthorization(auth =>
            //{
            //    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser().Build());
            //});

            //#endregion

            //#region Settings

            //var settings = new Settings();
            //new ConfigureFromConfigurationOptions<Settings>(
            //    Configuration.GetSection("Settings"))
            //                    .Configure(settings);

            //services.AddSingleton(settings);

            //#endregion

            //#region Businness Layers
            //services.AddTransient<RecorrenciaBL>();
            //#endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            
            //app.UseRouting();
            app.UseAuthorization();
            
            //app.MapControllers();
            
            app.Run();
           

            //ExceptionHandler
            //app.UseCustomExceptionHandler();

            
        }
    }
}
