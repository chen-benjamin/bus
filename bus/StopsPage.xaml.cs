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
    public partial class StopsPage : ContentPage
    {
        public NetworkingManager networkingManager = new NetworkingManager();
        private Route route;
        public StopsPage(Route route)
        {
            InitializeComponent();
            this.route = route;
        }

        private async void GoToDetailPage(object sender, EventArgs e)
        {
            var myListView = (ListView)sender;
            Stop stop = (Stop)myListView.SelectedItem;
            await Navigation.PushAsync(new DetailPage(stop));
        }

        protected override void OnAppearing()
        {
            StopList.ItemsSource = null;
            var list = networkingManager.GetStopsByRoute(this.route);
            StopList.ItemsSource = new ObservableCollection<Stop>(list);
            base.OnAppearing();

        }
    }
}