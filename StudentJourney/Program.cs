using ContosoJourney.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;
using StudentJourney.Interfaces;
using StudentJourney.Repository;
using StudentJourney.Services;
using FluentValidation.AspNetCore;
using ContosoJourney.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;




namespace ContosoJourney
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<JourneyContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<IEnrollmentRepository, EnrollmentsRepository>();
            builder.Services.AddScoped<IJourneysRepository, JourneysRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentsRepository>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IJourneyService, JourneyService>();
            builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

            builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EnrollmentValidator>());
            builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<JourneyValidator>());
            builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<StudentValidator>());

            builder.Services.AddAutoMapper(typeof(Journey));
            builder.Services.AddAutoMapper(typeof(Enrollment));
            builder.Services.AddAutoMapper(typeof(Student));

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<JourneyContext>();
            
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
		.AddEntityFrameworkStores<JourneyContext>();

			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("RequireAdministratorRole",
					 policy => policy.RequireRole("Administrator"));
                options.AddPolicy("RequireUserRole",
                    					 policy => policy.RequireRole("User"));
			});



			var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<JourneyContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/hi", () => "Hello!");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
