using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBOX.Authorization.RegistrationUC
{
    public class RegistrationWindowViewModel
    {
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
}
