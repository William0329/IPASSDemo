using IPASSDemo.Helpers;
using IPASSDemo.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;

using System.Reflection;

namespace IPASSDemo.Extensions
{
    /// <summary>
    /// ServiceCollection 擴充
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 註冊服務
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAPIService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddTransient<EncryptHelper>();
            services.AddTransient<JwtHelper>();
            return services;
        }
        /// <summary>
        /// 處理跨域請求
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCors(this IServiceCollection services, ConfigurationManager configuration)
        {
            var allowedHosts = configuration["AllowedCors"]?.Split(',').Select(h => h.Trim()).ToArray();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowedCors", builder =>
                {
                    if (allowedHosts != null && allowedHosts.Any())
                    {
                        builder.WithOrigins(allowedHosts)
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    }
                    else
                    {
                        builder.AllowAnyOrigin() // 如果配置中沒有設置則允許所有來源
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    }
                });
            });
            return services;
        }
        /// <summary>
        /// 註冊 Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Demo API",
                    Description = "A Demo API"
                });
                c.EnableAnnotations();
                c.ExampleFilters();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // pick comments from classes, include controller comments: another tip from StackOverflow
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                //add jwt UI
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization"
                    });
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
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
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            return services;
        }

        /// <summary>
        /// 註冊 驗證服務
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCertified(this IServiceCollection services, ConfigurationManager configuration)
        {
            // 加入JWT驗證
            services.AddAuthentication().AddJwtBearer($"Scheme", options =>
                {
                    // 當驗證失敗時，回應標頭會包含 WWW-Authenticate 標頭，這裡會顯示失敗的詳細錯誤原因
                    options.IncludeErrorDetails = true; // 預設值為 true，有時會特別關閉

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 透過這項宣告，就可以從 "sub" 取值並設定給 User.Identity.Name
                        //NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                        // 透過這項宣告，就可以從 "roles" 取值，並可讓 [Authorize] 判斷角色
                        //RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                        // 一般我們都會驗證 Issuer
                        ValidateIssuer = true,
                        ValidIssuer = configuration.GetValue<string>($"JwtSettings:Issuer"),

                        // 通常不太需要驗證 Audience
                        ValidateAudience = false,

                        // 一般我們都會驗證 Token 的有效期間
                        ValidateLifetime = true,

                        // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
                        ValidateIssuerSigningKey = false,

                        // SigningKey
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(configuration.GetValue<string>($"JwtSettings:Key")!))
                    };
                });

            return services;
        }
        /// <summary>
        /// 新增 Jwt 授權
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                var _jwtSchemePolicyBuilder = new AuthorizationPolicyBuilder($"Scheme");
                options.AddPolicy($"Demo", _jwtSchemePolicyBuilder
                        .RequireAuthenticatedUser()
                        .Build());
            });
            return services;
        }
    }
}
