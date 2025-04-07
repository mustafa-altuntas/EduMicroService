using Microsoft.Extensions.Options;

namespace EduMicroService.Catalog.Api.Options
{
    public static class OptionExt
    {
        public static IServiceCollection AddOptionsExt(this IServiceCollection services)
        {
            services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations().ValidateOnStart();

            services.AddSingleton<MongoOption>(sp=> sp.GetRequiredService<IOptions<MongoOption>>().Value); //herhangi bir yerde MongoOption kullanmak istediğimizde DI container ile istek talep karşılanacak



            return services;
        }
    }
}
