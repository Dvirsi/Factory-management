using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using Factory_Management.Models;

namespace Factory_Management.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        static UserBL useBL = new UserBL();
        // GET: api/User
        public IEnumerable<User> Get()
        {
            return useBL.GetAllUsers();
        }

        // GET: api/User/5
        public User Get(int id)
        {
            return useBL.GetUser(id);
        }

        // GET: api/User/5
        public User Get(string uname, string password)
        {
            try
            {
                return useBL.GetUserByUname(uname, password);
            }
            catch
            {
                return null;
            }
            
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public string Put(int id, User u)
        {
            useBL.updateAction(id, u);
            return "Updated!";
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
