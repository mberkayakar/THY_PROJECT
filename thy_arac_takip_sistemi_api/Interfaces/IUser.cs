using System.Collections.Generic;
using System.Linq;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Interfaces
{
    public interface IUser
    {
        IQueryable<User> GetAllUsers { get; }
        User Login(string email);
        POJO CreateUser(User user);
        POJO UpdateAuthority(User user);

        POJO DeleteUser(int id);
        POJO DeleteUsers(List<int> idList);
    }
}