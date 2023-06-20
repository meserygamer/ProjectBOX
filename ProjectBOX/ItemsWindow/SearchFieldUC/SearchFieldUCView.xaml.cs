using ProjectBOX.ItemsWindow.UserAreaUC;
using System;
using System.Collections.Generic;
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

namespace ProjectBOX.ItemsWindow.SearchFieldUC
{
    /// <summary>
    /// Логика взаимодействия для SearchFieldUCView.xaml
    /// </summary>
    public partial class SearchFieldUCView : UserControl
    {
        public static readonly DependencyProperty SearchQueryProperty = DependencyProperty.Register("SearchQuery", typeof(string), typeof(SearchFieldUCView));
        public static readonly DependencyProperty ClickOnSettingsButtonProperty = DependencyProperty.Register("ClickOnSettingsButton", typeof(RelayCommand), typeof(SearchFieldUCView));
        public static readonly DependencyProperty ClickOnSearchButtonProperty = DependencyProperty.Register("ClickOnSearchButton", typeof(RelayCommand), typeof(SearchFieldUCView));

        public string SearchQuery
        {
            get => (string)GetValue(SearchQueryProperty);
            set => SetValue(SearchQueryProperty, value);
        }

        public RelayCommand ClickOnSettingsButton
        {
            get => (RelayCommand)GetValue(ClickOnSettingsButtonProperty);
            set => SetValue(ClickOnSettingsButtonProperty, value);
        }

        public RelayCommand ClickOnSearchButton
        {
            get => (RelayCommand)GetValue(ClickOnSearchButtonProperty);
            set => SetValue(ClickOnSearchButtonProperty, value);
        }

        public SearchFieldUCView()
        {
            InitializeComponent();
        }
    }
}
