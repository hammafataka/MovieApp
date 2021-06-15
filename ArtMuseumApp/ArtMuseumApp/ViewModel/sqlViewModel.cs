using Movieapp.viewmodel;
using MovieApp.Models;
using MovieApp.ViewModel;
using MovieApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MovieApp.ViewModel
{
    public class sqlViewModel
    {
        public ObservableCollection<Result> sqlart { get; set; }
        public ICommand LoadSqlDataCommand { private set; get; }
        public ICommand DeleteItemCommand { private set; get; }
        public ICommand ShareItemCommand { private set; get; }
        public ICommand GoToDetailsCommand { private set; get; }


        public Result selectedArt { get; set; }
        public  async Task LoadSql()
        {
            List<Result> arts = await App.DbContext.GetItems<Result>();
            sqlart.Clear();
            foreach (Result art in arts)
            {
                sqlart.Add(art);
            }
        }

        private async Task Delete()
        {
            bool op;
            bool answer = await App.Current.MainPage.DisplayAlert("Confirm", "Do you Want to delete From favorites?", "Delete", "Cancel");
            if (answer)
            {
                op = await App.DbContext.DeleteItem<Result>(selectedArt);
                if (op)
                {
                    await LoadSql();
                    await App.Current.MainPage.DisplayAlert("success!", "Item Deleted From favorites...", "OK");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Error!", "Item Was NOT Deleted From favorites...", "OK");
            }
        }
        private async Task Share()
        {
            string answer = await App.Current.MainPage.DisplayActionSheet("choose...", "cancel", null, "via sms", "via email", "others");
            switch (answer)
            {

                case "cancel":

                    break;

                case "via sms":
                    await Sms.ComposeAsync(new SmsMessage("titile:\n" + selectedArt.title + "\n" + "presented date:\n" + selectedArt.release_date + "\nPopularity:\n" +
                        selectedArt.popularity + "\nimage url\n" + selectedArt.imageUrl,
                       "+420"));
                    break;
                case "via email":
                    await Email.ComposeAsync("art details", "titile:\n" + selectedArt.title + "\n" + "presented date:\n" + selectedArt.release_date + "\nPopularity:\n" +
                        selectedArt.popularity + "\nimage url\n" + selectedArt.imageUrl, "");
                    break;
                case "others":
                    await Xamarin.Essentials.Share.RequestAsync(new ShareTextRequest
                    {
                        Text = "titile:\n" + selectedArt.title + "\n" + "presented date:\n" + selectedArt.release_date + "\nPopularity:\n" +
                        selectedArt
                        .popularity + "\nimage url\n" + selectedArt.imageUrl,
                        Title = "share text"
                    });
                    break;
            }
        }
        private async Task GoToDetail()
        {
            if (selectedArt != null)
            {

                DetailsViewModel viewModel = new DetailsViewModel(selectedArt);
                ArtDetailsView view = new ArtDetailsView
                {
                    BindingContext = viewModel
                };
                await App.Current.MainPage.Navigation.PushAsync(view);
            }
        }

        public sqlViewModel()
        {
            sqlart = new ObservableCollection<Result>();
            Task.Run(async () => await LoadSql());
            LoadSqlDataCommand = new Command(async () => await LoadSql());
            ShareItemCommand = new Command(async () => await Share());
            DeleteItemCommand = new Command(async () => await Delete());
            GoToDetailsCommand = new Command(async ()=> await GoToDetail());
        }
    }
}
