using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Services
{
    // static olacak, diğer taraflardan erişim sağlanması için
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
        }
    }
}
