using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using bus.Model;
using Xamarin.Forms;

namespace bus
{
    public partial class MainPage : ContentPage
    {
        public NetworkingManager networkingManager = new NetworkingManager();
        public ObservableCollection<Route> routes;
        public MainPage()
        {
            InitializeComponent();
            var list = networkingManager.GetRoutes();
            this.routes = new ObservableCollection<Route>(list);
        }

        private async void GoToFavoritesPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavoritesPage());
        }

        private async void GoToRoutesPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RoutesPage(routes));
        }
    }
}
