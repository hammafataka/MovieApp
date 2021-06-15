using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovieApp.ViewModel
{
    public class BaseViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string PropertyName =null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        protected bool SetProperty<T>(ref T Storage, T Value, [CallerMemberName] string Propertyname=null)
        {
            if (object.Equals(Storage, Value))
                return false;
            Storage = Value;
            OnPropertyChanged(Propertyname);
            return true;
        }
        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
    }
}
