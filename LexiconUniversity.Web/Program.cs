﻿using Microsoft.EntityFrameworkCore;
using LexiconUniversity.Persistence.Data;
using LexiconUniversity.Web.Extensions;
using LexiconUniversity.Web.AutoMapperConfig;
using LexiconUniversity.Web.Services;

namespace LexiconUniversity.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<LexiconUniversityContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LexiconUniversityContext") ?? throw new InvalidOperationException("Connection string 'LexiconUniversityContext' not found.")));

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddAutoMapper(typeof(UniversityMappings));

        builder.Services.AddScoped<ICoursesService, CoursesService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            await app.SeedDataAsync();
        }
        else
        {
            app.UseExceptionHandler("/Students/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Students}/{action=Index}/{id?}");

        app.Run();
    }
}
