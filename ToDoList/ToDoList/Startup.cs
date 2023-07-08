using ToDoList.Adapters;
using ToDoList.Core.Application.Service;
using ToDoList.Core.Domains.ToDoList.Adapters.Repository;
using ToDoList.Core.Domains.ToDoList.Adapters.Service;


namespace ToDoList
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();

            
            services.AddScoped<IToDoListService, ToDoListService>();

            services.AddSingleton<IToDoListRepository, ToDoListRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();


            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}