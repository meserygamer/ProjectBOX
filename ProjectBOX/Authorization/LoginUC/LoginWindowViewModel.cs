using ProjectBOX.Authorization.RegistrationUC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjectBOX.Authorization.LoginUC
{
    class LoginWindowViewModel : INotifyPropertyChanged
    {
        //private Visibility _passwordWatermark;
        //public Visibility PasswordWatermark
        //{
        //    get => _passwordWatermark;
        //    set
        //    {
        //        _passwordWatermark = value;
        //        OnPropertyChanged("PasswordWatermark");
        //    }
        //}
        private int? _lenghPassword;
        private bool _keyBoardFocusStatus;

        public int? LenghPassword
        {
            get => _lenghPassword;
            set
            {
                _lenghPassword = value;
                OnPropertyChanged("LenghPassword");
            }
        }

        public bool KeyBoardFocusStatus
        {
            get => _keyBoardFocusStatus;
            set
            {
                _keyBoardFocusStatus = value;
                OnPropertyChanged("KeyBoardFocusStatus");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public partial class LoginWindowView
    {
        public static readonly DependencyProperty HyperLinkClickProperty = DependencyProperty.Register("HyperLinkClick", typeof(RelayCommand), typeof(LoginWindowView));

        public RelayCommand HyperLinkClick
        {
            get => (RelayCommand)GetValue(HyperLinkClickProperty);
            set => SetValue(HyperLinkClickProperty, value);
        }
    }

}
