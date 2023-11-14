using Syncfusion.UI.Xaml.NavigationDrawer;
using Syncfusion.UI.Xaml.Spreadsheet.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using WpfApp2.Common;
using WpfApp2.Menu.BC.User.View;
using WpfApp2.Menu.MainWindow.Model;
using WpfApp2.Menu.SerialConnection.ViewModel;
using MenuItem = WpfApp2.Menu.MainWindow.Model.MenuItem;

namespace WpfApp2.Menu.MainWindow.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<MenuItem>? MenuItems { get; set; } = new();
        public ObservableCollection<MenuItem>? SubItems { get; set; } = new();

        public MainWindowViewModel()
        {
            List<BC_MENU> menus = new()
            {
                new() { MENU_TITLE = "Base" },
                new() { MENU_TITLE = "User", URI = "/Menu/BC/User/View/UserPage.xaml", P_MENU = "Base" },
                new() { MENU_TITLE = "Tank", P_MENU = "Base" },
                new() { MENU_TITLE = "Vessel", P_MENU = "Base" },
                new() { MENU_TITLE = "Serial" },
                new() { MENU_TITLE = "Serial Connection", URI = "/Menu/SerialConnection/View/SerialConnection.xaml", P_MENU = "Serial" },
                new() { MENU_TITLE = "Title - 1" },
                new() { MENU_TITLE = "Title - 2" }
            };

            List<BC_MENU> pMenus = menus.Where(x => string.IsNullOrEmpty(x.P_MENU)).ToList();
            foreach (var parent in pMenus)
            {
                MenuItem parentItem = new()
                {
                    MenuTitle = parent.MENU_TITLE
                };

                SubItems = new ObservableCollection<MenuItem>();
                var cMenus = menus.Where(x => x.P_MENU == parent.MENU_TITLE).ToList();
                foreach (var child in cMenus)
                {
                    MenuItem childItem = new()
                    {
                        MenuTitle = child.MENU_TITLE,
                        PMenu = child.P_MENU,
                        URI = child.URI,
                    };

                    SubItems.Add(childItem);
                }

                parentItem.SubItems = SubItems;
                MenuItems.Add(parentItem);
            }
        }

        public void MainWindow_Closed(object sender, EventArgs e)
        {
            
            if (((((((sender as Window)!.Content 
                             as Grid)!.Children[0] 
                             as SfNavigationDrawer)!.ContentView 
                             as Grid)!.Children[0] 
                             as Frame)!.Content 
                             as Page)!.DataContext 
                             is SerialConnectionViewModel viewModel)
            {
                viewModel.SerialClose();
            }
        }

        public void SfNavigationDrawer_ItemClicked(object sender, NavigationItemClickedEventArgs e)
        {
            MenuItem? menuItem = e.Item.Header as MenuItem;

            if (string.IsNullOrEmpty(menuItem?.PMenu))
            { }
            else if (string.IsNullOrEmpty(menuItem?.URI))
            {
                (((sender as SfNavigationDrawer)!.ContentView as Grid)!.Children[0] as Frame)!.Navigate(new Uri("/Menu/ETC/NotFoundPage.xaml", UriKind.Relative));
            }
            else
            {
                (((sender as SfNavigationDrawer)!.ContentView as Grid)!.Children[0] as Frame)!.Navigate(new Uri(menuItem.URI, UriKind.Relative));
            }
        }

        public void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Page currentPage)
            {
                if (currentPage.GetType().Name == "SerialConnection")
                {
                    currentPage.Unloaded += SerialConncetionPage_Unloaded;
                }
            }
        }

        private void SerialConncetionPage_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is Page page)
            {
                if (page.DataContext is SerialConnectionViewModel viewModel)
                {
                    viewModel.SerialClose();
                }
            }
        }
    }
}
