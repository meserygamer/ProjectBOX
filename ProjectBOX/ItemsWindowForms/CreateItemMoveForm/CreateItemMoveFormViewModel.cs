using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBOX.ItemsWindowForms.CreateItemMoveForm
{
    public class CreateItemMoveFormViewModel : INotifyPropertyChanged
    {
        #region UserDateTime DateTime
        private DateTime _userDateTime;

        public DateTime UserDateTime
        {
            get { return _userDateTime; }
            set {
                _userDateTime = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region MovementDescription string
        private string _movementDescription;

        public string MovementDescription
        {
            get { return _movementDescription; }
            set
            {
                _movementDescription = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region INotifyPropertyChanged Realize
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
