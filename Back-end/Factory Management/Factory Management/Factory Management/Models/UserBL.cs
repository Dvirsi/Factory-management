using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory_Management.Models
{
    public class UserBL
    {
        Factory_Management_DBEntities1 db = new Factory_Management_DBEntities1();

        List<User> users = new List<User>();

        public List<User> GetAllUsers()
        {
            return db.Users.ToList();
        }

        public User GetUser(int id)
        {
            return db.Users.Where(x => x.ID == id).First();
        }
        
        public User GetUserByUname(string uname, string password)
        {
            var users = GetAllUsers();
            return users.Where(x => x.UserName == uname && x.Password == password).First();
        }



        public void updateAction(int id, User u)
        {
            var user = db.Users.Where(x => x.ID == id).First();
            user.FullName = u.FullName;
            user.UserName = u.UserName;
            user.Password = u.Password;
            user.NumOfAction = u.NumOfAction;
            user.ActionDone = u.ActionDone;

            DateTime localDate = DateTime.UtcNow.Date;

            if (user.LastActionDate != localDate)
            {
                user.LastActionDate = localDate;
                user.ActionDone = 0;
            }
            db.SaveChanges();
        }
    }
}