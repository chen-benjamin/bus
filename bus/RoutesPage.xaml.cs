using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoutesPage : ContentPage
    {
        public RoutesPage(ObservableCollection<Route> routes)
        {
            InitializeComponent();
            RouteList.ItemsSource = routes;
        }

        private async void GoToStopsPage(object sender, EventArgs e)
        {
            var myListView = (ListView)sender; 
            Route route = (Route)myListView.SelectedItem;
            await Navigation.PushAsync(new StopsPage(route));
        }
    }
}