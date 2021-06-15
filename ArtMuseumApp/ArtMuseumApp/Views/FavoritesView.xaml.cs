using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MovieApp.ViewModel;
using MovieApp.Models;
namespace MovieApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesView : ContentPage
    {
        sqlViewModel vm;
        public FavoritesView()
        {
            InitializeComponent();
            vm = new sqlViewModel();
            this.BindingContext = vm;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Run(() => vm.LoadSqlDataCommand.Execute(null));
        }
    }
}