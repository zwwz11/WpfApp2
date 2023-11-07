using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Menu.BC.User.Repository
{
    public class UserRepository
    {
        private static List<Model.User> users = new();

        public UserRepository()
        {
            users = Enumerable.Range(1, 100).Select(x => new Model.User()
            {
                ID = x,
                Name = x.ToString(),
                Address = x.ToString(),
                Age = x
            }).ToList();
        }

        public List<Model.User> GetAll()
        {
            return users;
        }

        public List<Model.User> GetAll(Model.User filter)
        {
            var filterUsers = users.Where(x => x.Name!.Contains($"{filter.Name}")).ToList();
            filterUsers = filterUsers.Where(x => x.Address!.Contains($"{filter.Address}")).ToList();

            return filterUsers;
        }

        public void Create(Model.User user)
        {
            users.Add(user);
        }

        public void Update(Model.User user)
        {
            Model.User? target = users.FirstOrDefault(x => x.ID == user.ID);
            if (target != null)
            {
                target.Name = user.Name;
                target.Age = user.Age;
                target.Address = user.Address;
            }
        }

        public void Delete(Model.User user)
        {
            users.Remove(user);
        }

        public Model.User? FindById(Model.User user)
        {
            return users.Find(x => x.ID == user.ID);
        }

        public int? GetMaxSequence()
        {
            if (users.Count == 0)
                return 1;

            return users.Last().ID + 1;
        }
    }
}
