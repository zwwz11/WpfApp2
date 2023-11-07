using Syncfusion.UI.Xaml.NavigationDrawer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp2.Common;
using WpfApp2.Menu.BC.User.View;
using WpfApp2.Menu.MainWindow.Model;
using MenuItem = WpfApp2.Menu.MainWindow.Model.MenuItem;

namespace WpfApp2.Menu.MainWindow.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<MenuItem>? Menus { get; set; } = new();

        public MainWindowViewModel()
        {
            Menus.Add(new() { MenuTitle = "Menu1", URI = "/Menu/BC/User/View/UserPage.xaml" });
            Menus.Add(new() { MenuTitle = "Menu2" });
            Menus.Add(new() { MenuTitle = "Menu3" });
            Menus.Add(new() { MenuTitle = "Menu4" });
            Menus.Add(new() { MenuTitle = "Menu5" });
        }

        public void Test(object sender, NavigationItemClickedEventArgs e)
        {
            string? uri = (e.Item.Header as MenuItem)?.URI;

            if (string.IsNullOrEmpty(uri))
            {
                (((sender as SfNavigationDrawer)!.ContentView as Grid)!.Children[0] as Frame)!.Navigate(new Uri("/Menu/ETC/NotFoundPage.xaml", UriKind.Relative));
            }
            else
            {
                (((sender as SfNavigationDrawer)!.ContentView as Grid)!.Children[0] as Frame)!.Navigate(new Uri(uri, UriKind.Relative));
            }
        }
    }
}
