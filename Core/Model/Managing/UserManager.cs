using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DbControl;
using Core.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace Core.Model.Managing
{
    public class UserManager
    {

        public event EventHandler UserCreated;
        public event EventHandler UserStatusChanged;

        public UserManager(DbSet<Client> clients)
        {
        }

        public void AddUser(string name, string secondName)
        {
            using UserDbContext dbContext = new UserDbContext();
            try
            {
                Client newClient = new Client() {Name = name, SecondName = secondName};
                dbContext.Clients.Add(newClient);
                dbContext.SaveChanges();
                UserCreated?.Invoke(this, new UserEventArgs(newClient));
            }
            catch (Exception e)
            {
                throw new Exception("Client adding failed", e);
            }
        }


    }
}
