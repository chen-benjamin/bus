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
    public partial class DetailPage : ContentPage
    {
        public NetworkingManager networkingManager = new NetworkingManager();
        private Stop stop;
        private DBManager db = new DBManager();
        public DetailPage(Stop stop)
        {
            InitializeComponent();
            this.stop = stop;
        }

        private async void GotoAddOrUpdatePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddOrUpdatePage(this.stop));
        }

        protected override void OnAppearing()
        {
            Title.Text = this.stop.Title;
            BusList.ItemsSource = null;
            var list = networkingManager.GetBusesByStop(this.stop);
            BusList.ItemsSource = new ObservableCollection<Bus>(list);
            bool visible = true;
            if (stop.Id != null && db.CheckIfStopExist(stop)) visible = false;
            AddBtn.IsVisible = visible;
            base.OnAppearing();

        }
    }
}