using Microsoft.EntityFrameworkCore;
using Proiect_An.Data;
using Proiect_An.Models.DesignPatterns.FactoryMethod;

namespace Proiect_An
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IRoomFactory, ConcreteRoomFactory>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MyAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Booking}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
