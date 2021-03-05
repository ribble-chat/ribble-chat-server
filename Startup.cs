using System;
using RibbleChatServer.Services;
using RibbleChatServer.GraphQL.ResultTypes;
using HotChocolate;
using StackExchange.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RibbleChatServer.Data;
using Microsoft.AspNetCore.Identity;
using RibbleChatServer.Models;
using RibbleChatServer.GraphQL;
using HotChocolate.Types;
using HotChocolate.AspNetCore;

namespace RibbleChatServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) => (Configuration, Env) = (configuration, env);

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // Cassandra is thread-safe I think
            services.AddSingleton<IMessageDb, MessageDb>();
            services.AddAuthentication();
            services.AddSignalR();
            services
                .AddDbContext<MainDbContext>(options => options
                .UseNpgsql(Configuration.GetConnectionString("ChatDbContext"))
                .UseSnakeCaseNamingConvention());

            services
                .AddIdentity<User, Models.Role>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MainDbContext>()
                .AddDefaultTokenProviders();

            services.AddRedisSubscriptions(_ => ConnectionMultiplexer.Connect("ribble-redis"));

            services.AddScoped<Query>();
            services.AddScoped<Mutation>();
            services
                .AddGraphQLServer()
                .EnableRelaySupport()
                .AddType(new UuidType(defaultFormat: 'B'))
                .AddType<UserType>()
                .AddType<GroupType>()
                .AddType<LoginSuccess>()
                .AddType<LoginUnknownUserError>()
                .AddType<LoginIncorrectPasswordError>()
                .AddQueryType<QueryType>()
                .AddMutationType<MutationType>()
                .AddSubscriptionType<SubscriptionType>()
                .AddErrorFilter<GraphQLErrorFilter>();


            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                // disable identity validation and use zxcvbn
                options.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false,
                    RequiredLength = 0,
                    RequireLowercase = false,
                    RequiredUniqueChars = 1,

                };

                options.Lockout = new LockoutOptions
                {
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
                    MaxFailedAccessAttempts = 5,
                    AllowedForNewUsers = true,
                };

                options.User = new UserOptions
                {
                    AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+",
                    RequireUniqueEmail = true,
                };
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/api/identity/account/login";
                options.SlidingExpiration = true;
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                });
            });

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RibbleChatServer", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RibbleChatServer v1"));
            }

            // app.UseHttpsRedirection();
            app.UseWebSockets();
            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGraphiQLServer();
            app.UseGraphQLAltair();
            app.UseGraphQLVoyager();
            app.UsePlayground();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}
