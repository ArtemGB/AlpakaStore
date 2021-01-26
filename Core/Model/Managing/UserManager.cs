using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace Core.Model.Managing
{
    public class UserManager
    {
        private DbSet<Client> clients;

        public event EventHandler UserCreated;
        public event EventHandler UserStatusChanged;

        public UserManager(DbSet<Client> clients)
        {
            this.clients = clients;
        }

        public void AddUser()
        {

        }
    }
}
