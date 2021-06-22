using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrUpdatePage : ContentPage
    {
        private DBManager db = new DBManager();
        private Stop stop;
        public AddOrUpdatePage(Stop stop)
        {
            InitializeComponent();
            this.stop = stop;
            Name.Text = stop.Name;
            Title.Text = stop.Title;
        }

        private async void SaveFavorite(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Name.Text))
            {
                _ = DisplayAlert("Error", "Plese select the vaccine!", "OK");
            }
            else
            {
                this.stop.Name = Name.Text;
                Console.WriteLine(Name.Text);
                db.InsertOrUpdateStop(this.stop);
                await DisplayAlert("Success", "Favorite stop is saved!", "Ok");
                await Navigation.PopAsync();
            }
        }
    }
}