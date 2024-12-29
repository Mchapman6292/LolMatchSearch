namespace API.Configuration.APIStartups
{
    public class APIStartup
    {
        public IConfiguration Configuration { get; }


        public APIStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();           // Enables Swagger API documentation
                app.UseSwaggerUI();         // Enables the Swagger UI web interface

            }

            app.UseHttpsRedirection();      // Redirects HTTP requests to HTTPS

            app.UseRouting();              // Matches request to an endpoint(TestController)

            app.UseAuthorization();         // Checks if the request is authorized
        
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

