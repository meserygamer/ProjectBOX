using ProjectBOX.EntityFrameworkModelFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectBOX.ItemsWindowForms.AddingItemToListUC
{
    /// <summary>
    /// Логика взаимодействия для AddingItemToListUCView.xaml
    /// </summary>
    public partial class AddingItemToListUCView : UserControl
    {
        #region ListOfSelectedItemsOnMove ObservableCollection<ObjectDatum> DependencyProperty
        public static readonly DependencyProperty ListOfSelectedItemsOnMoveProperty =
            DependencyProperty.Register("ListOfSelectedItemsOnMove", typeof(ObservableCollection<ObjectDatum>), typeof(AddingItemToListUCView));

        public ObservableCollection<ObjectDatum> ListOfSelectedItemsOnMove
        {
            get => (ObservableCollection<ObjectDatum>)GetValue(ListOfSelectedItemsOnMoveProperty);
            set => SetValue(ListOfSelectedItemsOnMoveProperty, value);
        }
        #endregion

        public AddingItemToListUCView()
        {
            ListOfSelectedItemsOnMove = new ObservableCollection<ObjectDatum>();
            InitializeComponent();
        }
    }
}
