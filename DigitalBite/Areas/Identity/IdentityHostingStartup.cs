using System;
using DigitalBite.Areas.Identity.Data;
using DigitalBite.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DigitalBite.Areas.Identity.IdentityHostingStartup))]
namespace DigitalBite.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Admin>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AdminConnection")));

                services.AddDefaultIdentity<DigitalBiteAdmin>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Admin>();
            });
        }
    }
}