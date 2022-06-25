using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;
using thy_arac_takip_sistemi_api.Utils;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class UserRepo : IUser
    {
        private readonly MyDbContext db;

        public UserRepo(MyDbContext _db)
        {
            db = _db;
        }

        IQueryable<User> IUser.GetAllUsers => db.Users;

        public POJO CreateUser(User user)
        {
            POJO model = new POJO
            {
                Flag = false
            };

            try
            {
                User usr = db.Users.FirstOrDefault(e => e.userId == user.userId);
                if (usr != null)
                {
                    model.Message = "User exist";
                    return model;
                }

                user.dateCreated = DateTime.Now;
                user.dateLastLogin = DateTime.Now;
                db.Users.Add(user);

                db.SaveChanges();

                model.Flag = true;
                model.Message = "Kullanıcı eklendi";

            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }

            return model;
        }

        POJO IUser.DeleteUser(int id)
        {

            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                User entry = db.Users.FirstOrDefault(e => e.id == id);
                if (entry != null)
                {
                    db.Users.Remove(entry);
                    db.SaveChanges();
                    model.Message = "Başarıyla silindi.";
                    model.Flag = true;
                }
                else
                {
                    model.Message = "Böyle bir kayıt bulunamadı";

                }
            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }
            return model;
        }

        POJO IUser.DeleteUsers(List<int> idList)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                foreach (var item in idList)
                {
                    User pts = db.Users.FirstOrDefault(e => e.id == item);
                    if (pts != null)
                    {
                        db.Users.Remove(pts);
                        db.SaveChanges();
                        model.Message = "Silindi";
                        model.Flag = true;
                    }
                }

            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }
            return model;
        }

        User IUser.Login(string email)
        {
            try
            {
                User _user = db.Users.FirstOrDefault(e => e.email == email);
                return _user;


            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
                throw;
            }



        }
        POJO IUser.UpdateAuthority(User user)
        {
            POJO model = new POJO
            {
                Flag = false
            };

            try
            {
                User usr = db.Users.FirstOrDefault(e => e.id == user.id);
                if (usr == null)
                {
                    model.Message = "User not found";
                    return model;
                }


                usr.dateUpdated = DateTime.Now;
                usr.authority = user.authority;
                db.SaveChanges();
                model.Flag = true;
                model.Message = "Kullanıcı güncellendi";

            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }

            return model;
        }


    }
}
