using ProductShop.Connection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProductShop.Cookie
{
    public class Session : INotifyPropertyChanged
    {
        #region SingleTon realization
        private static Session _session;
        public static Session Instance => _session ?? (_session = new Session());

        private Session() {}
        #endregion
        #region INotifyPropertyChanged interface realization
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion

        private User _user;

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

    }
}
