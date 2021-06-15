using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;


using Xamarin.Forms;
using System.Threading.Tasks;
using MovieApp.Services;
using MovieApp.Models;
using MovieApp.Views;
using Movieapp.viewmodel;

namespace MovieApp.ViewModel
{
    public class ListViewModel:BaseViewModel
    {
        public ObservableCollection<Result> movies { get; }
        private List<Result> movieList;
        private string movieName;
        public string MovieName
        {
            get { return movieName; }
            set {SetProperty(ref movieName, value); }
        }
        private Result selectedArt;

        public Result SelectedArt
        {
            get { return selectedArt; }
            set { SetProperty(ref selectedArt , value); }
        }

        public ICommand LoadDataCommand { private set; get; }
        public ICommand GoToDetailsCommand { private set; get; }
        public async void LoadData()
        {
            IsBusy = true;
            movies.Clear();
            movieList = await ApiService.GetArts(MovieName);
            foreach (Result art in movieList.Take(5))
            {
                movies.Add(art);
            }
            IsBusy = false;
        }
        
        private async Task GoToDetail()
        {
            if (SelectedArt != null)
            {

                DetailsViewModel viewModel = new DetailsViewModel(SelectedArt);
                ArtDetailsView view = new ArtDetailsView
                {
                    BindingContext = viewModel
                };
                await App.Current.MainPage.Navigation.PushAsync(view);
            }
        }


        public ListViewModel()
        {
            movieList = new List<Result>();
            movies = new ObservableCollection<Result>();
            LoadDataCommand = new Command(LoadData);
            GoToDetailsCommand = new Command(async () => await GoToDetail());

        }


    }
}
