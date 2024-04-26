using LexiconUniversity.Persistence;
using LexiconUniversity.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LexiconUniversity.Web.Extensions;

public static class WebAppExtensions
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var context = serviceProvider.GetRequiredService<LexiconUniversityContext>();

        //await context.Database.EnsureDeletedAsync();
        //await context.Database.MigrateAsync();

        try
        {
            await SeedData.InitAsync(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
