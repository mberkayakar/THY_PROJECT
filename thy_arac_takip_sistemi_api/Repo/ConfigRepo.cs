using System;
using System.Linq;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class ConfigRepo : IConfig
    {
        private readonly MyDbContext db;
        public ConfigRepo(MyDbContext _db)
        {
            db = _db;
        }

        IQueryable<Config> IConfig.GetConfigs => db.Configs;

        POJO IConfig.UpdateConfig(Config config)
        {
            POJO model = new POJO { Flag = false };
            Config _config = db.Configs.Where((e) => e.key == config.key).FirstOrDefault();

            try
            {
                if (_config != null)
                {
                    _config.value = config.value;
                    db.SaveChanges();
                    model.Flag = true;
                    model.Message = "Başarıyla Güncellendi";
                }
                else
                {
                    model.Message = "Böyle bir kayıt bulunamadı";

                }

            }
            catch (Exception e)
            {
                model.Message = e.ToString();
            }

            return model;
        }
    }
}
