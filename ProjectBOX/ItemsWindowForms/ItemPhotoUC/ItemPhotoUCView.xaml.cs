using ProjectBOX.Authorization.LoginUC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ProjectBOX.ItemsWindowForms.ItemPhotoUC
{
    public partial class ItemPhotoUCView : UserControl
    {
        public static readonly DependencyProperty ByteArrayFormOfImageProperty = DependencyProperty.Register("ByteArrayFormOfImage", typeof(byte[]), typeof(ItemPhotoUCView));

        public byte[] ByteArrayFormOfImage
        {
            get {
                return (byte[])GetValue(ByteArrayFormOfImageProperty);
            }
            set {
                SetValue(ByteArrayFormOfImageProperty, value);
            }
        }

        public ItemPhotoUCView()
        {
            InitializeComponent();
        }
    }
}
