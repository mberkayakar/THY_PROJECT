using System.Linq;
using thy_arac_takip_sistemi_api.Repo;

public static class DatabaseInitializer
{
    public static void Initialize(MyDbContext db)
    {
        db.Database.EnsureCreated();
        if (!db.Configs.Any())
        {
            db.Configs.Add(
                new thy_arac_takip_sistemi_api.Models.Config { key = "RESERVATION_DELAY", value = "3600" }
            ); db.Configs.Add(
                new thy_arac_takip_sistemi_api.Models.Config { key = "BUTTON_DELAY", value = "7200" }
         );
        }
    }
}