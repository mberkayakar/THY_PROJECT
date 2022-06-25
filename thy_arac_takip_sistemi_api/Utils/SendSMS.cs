using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using thy_arac_takip_sistemi_api.Repo;

namespace thy_arac_takip_sistemi_api.Utils
{
    public class SMSContext
    {
        MyDbContext dbContext;
        public SMSContext(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public string GetConfigWithKey(string key)
        {
            return
             dbContext.Configs.FirstOrDefault(e => e.key == key).value;
        }
        async public void SendSMS(string phoneNumber, string text)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string url = GetConfigWithKey("SMS_URL");
                string username = GetConfigWithKey("SMS_USERNAME");
                string password = GetConfigWithKey("SMS_PASSWORD");
                string basicuser = GetConfigWithKey("SMS_BASIC_AUTH_USERNAME");
                string basicpass = GetConfigWithKey("SMS_BASIC_AUTH_PASSWORD");
                //Adding basit authenticate header

                var authenticationString = $"{basicuser}:{basicpass}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);

                System.Console.WriteLine(base64EncodedAuthenticationString);
                var configs = new
                {
                    username = username,
                    password = password,
                    userId = username,
                    messages =

                    new object[]{
                            new{

                                    index=1,
                                    number=905356810000,
                                message="test"

                            }
                         },
                };
                var jsonData = JsonConvert.SerializeObject(configs, Formatting.Indented);
                System.Console.WriteLine(jsonData);
                var res = await httpClient.PostAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json"));
                //    res.Content.ReadAsStringAsync().Result);
                System.Console.WriteLine(

                    res.Content.ReadAsStringAsync().Result
                );

                return;


            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return;
            }
        }
        //fileModel save to db

    }
}
