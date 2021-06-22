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
    public partial class FavoritesPage : ContentPage
    {
        private DBManager db = new DBManager();
        public ObservableCollection<Stop> stops;
        public FavoritesPage()
        {
            InitializeComponent();
        }

        private async void GoToDetailPage(object sender, EventArgs e)
        {
            var myListView = (ListView)sender;
            Stop stop = (Stop)myListView.SelectedItem;
            await Navigation.PushAsync(new DetailPage(stop));
        }

        private async void GotoAddOrUpdatePage(object sender, EventArgs e)
        {
            Stop stop = (sender as MenuItem).CommandParameter as Stop;
            await Navigation.PushAsync(new AddOrUpdatePage(stop));
        }

        private void DeleteFavorite(object sender, EventArgs e)
        {
            Stop stop = (sender as MenuItem).CommandParameter as Stop;
            db.DeleteStop(stop);
            this.stops.Remove(stop);
            _ = DisplayAlert("Success", "Stop deleted!!", "OK");
        }

        protected async override void OnAppearing()
        {
            this.stops = await db.CreateTable();
            StopList.ItemsSource = this.stops;
            base.OnAppearing();
        }
    }
}