using MovieApp;
using MovieApp.Models;
using MovieApp.ViewModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Movieapp.viewmodel
{
    public class DetailsViewModel : BaseViewModel
    {

        private Result selectedart;
        public Result localobject
        {
            get { return selectedart; }
            set { SetProperty(ref selectedart, value); }
        }

        public ICommand additemcommand { private set; get; }
        public ICommand shareitemcommand { private set; get; }
        private async Task AddItem()
        {
            bool op;

            op = await App.DbContext.AddItem(localobject);
            if (op)
            {
                sqlViewModel vm = new sqlViewModel();
                await vm.LoadSql();
                await Application.Current.MainPage.DisplayAlert("success!", "item added to favorites...", "ok");

            }
            else
                await Application.Current.MainPage.DisplayAlert("error!", "item was not added to favorites...", "ok");
        }
        private async Task Share()
        {
            string answer = await App.Current.MainPage.DisplayActionSheet("choose...", "cancel", null, "via sms", "via email", "others");
            switch (answer)
            {

                case "cancel":

                    break;

                case "via sms":
                    await Sms.ComposeAsync(new SmsMessage("titile:\n" + localobject.title + "\n" + "presented date:\n" + localobject.release_date + "\nPopularity:\n" +
                        localobject.popularity + "\nimage url\n" + localobject.imageUrl,
                       "+420"));
                    break;
                case "via email":
                    await Email.ComposeAsync("art details", "titile:\n" + localobject.title + "\n" + "presented date:\n" + localobject.release_date + "\nPopularity:\n" +
                        localobject.popularity + "\nimage url\n" + localobject.imageUrl, "");
                    break;
                case "others":
                    await Xamarin.Essentials.Share.RequestAsync(new ShareTextRequest
                    {
                        Text = "titile:\n" + localobject.title + "\n" + "presented date:\n" + localobject.release_date + "\nPopularity:\n" +
                        localobject.popularity + "\nimage url\n" + localobject.imageUrl,
                        Title = "share text"
                    });
                    break;
            }
        }
        public DetailsViewModel(Result artobject)
        {
            localobject = artobject;
            additemcommand = new Command(async () => await AddItem());
            shareitemcommand = new Command(async () => await Share());

        }
    }
}
