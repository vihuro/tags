namespace api.tags.Config
{
    public static class ConfigurationMongoDbContext
    {
        public static void ConfigureMongoDbContext(this IServiceCollection services,
                                                IConfiguration configuration)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environmentName}.json")
                .Build();

            services.Configure<ConnectionMongo>(op =>
            {
                op.ConnectionString = configuration.GetSection("MongoConnection:ConnectionString").Value;
                op.Database = configuration.GetSection("MongoConnection:Database").Value;
            });

        }
    }
}
