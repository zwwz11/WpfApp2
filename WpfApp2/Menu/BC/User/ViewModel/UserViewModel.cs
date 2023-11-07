using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Common;
using WpfApp2.Menu.BC.User.Service;

namespace WpfApp2.Menu.BC.User.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        private readonly UserService userService;
        private readonly RelayCommand? readCommand;
        private readonly RelayCommand? createCommand;
        private readonly RelayCommand? updateCommand;
        private readonly RelayCommand? deleteCommand;
        private readonly RelayCommand? newCommand;

        public RelayCommand ReadCommand => readCommand ?? new RelayCommand(Search);
        public RelayCommand CreateCommand => createCommand ?? new RelayCommand(Create);
        public RelayCommand UpdateCommand => updateCommand ?? new RelayCommand(Update);
        public RelayCommand DeleteCommand => deleteCommand ?? new RelayCommand(Delete);
        public RelayCommand NewCommand => newCommand ?? new RelayCommand(New);


        private ObservableCollection<Model.User> users = new();
        private Model.User currentUser = new();

        public ObservableCollection<Model.User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        public Model.User CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }



        public UserViewModel()
        {
            userService = new();
            Search();
        }

        private void Search()
        {
            Users = new ObservableCollection<Model.User>(userService.GetAllUsers(currentUser));
        }

        private void Create()
        {
            userService.CreateUser(currentUser);
            Search();
        }

        private void Update()
        {
            userService.UpdateUser(currentUser);
        }

        private void Delete()
        {
            userService.DeleteUser(currentUser);
            Search();
        }

        private void New()
        {
            CurrentUser = new()
            {
                ID = int.MinValue
            };
        }
    }
}
