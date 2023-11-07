using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Menu.BC.User.Repository;

namespace WpfApp2.Menu.BC.User.Service
{
    public class UserService
    {
        private readonly UserRepository userRepository;
        public UserService()
        {
            userRepository = new UserRepository();
        }

        public List<Model.User> GetAllUsers()
        {
            return userRepository.GetAll();
        }

        public List<Model.User> GetAllUsers(Model.User filter)
        {
            return userRepository.GetAll(filter);
        }

        public void CreateUser(Model.User user)
        {
            if (user.ID != int.MinValue)
                return;

            user.ID = userRepository.GetMaxSequence();
            userRepository.Create(user);
        }

        public void UpdateUser(Model.User user)
        {
            Model.User? target = userRepository.FindById(user);
            if (target != null)
            {
                userRepository.Update(user);
            }
        }

        public void DeleteUser(Model.User user)
        {
            userRepository.Delete(user);
        }

        public Model.User? FindUserById(Model.User user)
        {
            return userRepository.FindById(user);
        }

        public int? GetMaxUserSequence()
        {
            return userRepository.GetMaxSequence();
        }
    }
}
