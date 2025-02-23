namespace ClubAccessSystem.API.Configurations
{
    public static class Cors
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });

            });
        }
    }
}
