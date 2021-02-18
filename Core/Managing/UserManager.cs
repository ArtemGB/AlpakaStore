using Core.DbControl;
using Core.Model.Users;
using System;

namespace Core.Managing
{

    public class UserManager
    {

        public event EventHandler UserCreated;
        public event EventHandler UserStatusChanged;

        public UserManager()
        {
        }

        public void AddClient(string name, string secondName)
        {
            using StoreDbContext dbContext = new StoreDbContext();
            Client newClient = new Client() { Name = name, SecondName = secondName };
            try
            {
                dbContext.Clients.Add(newClient);
                dbContext.SaveChanges();
                UserCreated?.Invoke(this, new UserEventArgs(newClient));
            }
            catch (Exception e)
            {
                throw new Exception("User creation error", e);
            }
        }


    }
}
