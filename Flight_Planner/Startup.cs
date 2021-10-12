using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Flight_Planner.Helpers;
using Microsoft.EntityFrameworkCore;
using FlightPlanner.Data;
using FlightPlanner.Core.Services;
using FlightPlanner.Services;
using FlightPlanner.Core.Models;
using Flight_Planner.Mapping;
using AutoMapper;
using FlightPlanner.Services.Validation;
using FlightPlanner.Services.Validation.FlightSearch;

namespace Flight_Planner
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, AuthenticateHandler>("BasicAuthentication", null);
            services.AddDbContext<FlightPlannerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("flight-planner")));
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Flight_Planner", Version = "v1" });
            });

            services.AddScoped<IFlightPlannerDbContext, FlightPlannerDbContext>();
            services.AddScoped<IDbService,DbService>();
            services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
            services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
            services.AddScoped<IDbServiceExtended, DbServiceExtended>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IValidator, CityValidator>();
            services.AddScoped<IValidator, AirportCodeValidator>();
            services.AddScoped<IValidator, ArrivalTimeValidator>();
            services.AddScoped<IValidator, CarrierValidator>();
            services.AddScoped<IValidator, CountryValidator>();
            services.AddScoped<IValidator, DepartureTimeValidator>();
            services.AddScoped<IValidator, TravelTimeValidator>();
            services.AddScoped<IFlightSearchService, FlightSearchService>();
            services.AddScoped<IValidator, AirportConnectValidator>();
            services.AddScoped<IPageResultService, PageResultService>();
            services.AddScoped<ISearchValidator, SearchAirportValidator>();


            var cfg = AutoMapperConfiguration.GetConfig();
            services.AddSingleton(typeof(IMapper),cfg);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flight_Planner v1"));
            }

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
