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

namespace ProjectBOX.Authorization.LoginUC
{
    /// <summary>
    /// Логика взаимодействия для LoginWindowView.xaml
    /// </summary>
    public partial class LoginWindowView : UserControl
    {
        public LoginWindowView()
        {
            InitializeComponent();
        }
    }
    public class WaterMarkTextBox : TextBox
    {
        #region Fields

        private const string _defaultWatermark = "None";

        public static readonly DependencyProperty WatermarkTextProperty = DependencyProperty.Register("WatermarkText", typeof(string), typeof(WaterMarkTextBox), new UIPropertyMetadata(string.Empty, OnWatermarkTextChanged));

        #endregion

        #region Constructor(s)
        public WaterMarkTextBox()
          : this(_defaultWatermark)
        {
        }
        public WaterMarkTextBox(string watermark)
        {
            WatermarkText = watermark;
        }

        #endregion

        #region Properties

        public string WatermarkText
        {
            get { return (string)GetValue(WatermarkTextProperty); }
            set { SetValue(WatermarkTextProperty, value); }
        }

        #endregion

        #region Methods

        public static void OnWatermarkTextChanged(DependencyObject box, DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion
    }
}
