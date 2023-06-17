using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Security.Policy;
using System.Security.Cryptography;

namespace ProjectBOX.Authorization
{
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
    public class PasswordBoxBehavior : Behavior<PasswordBox>
    {
        public static readonly DependencyProperty LenghPasswordProperty;
        public static readonly DependencyProperty SecurePasswordProperty;
        public static readonly DependencyProperty KeyBoardFocusProperty;

        static PasswordBoxBehavior()
        {
            KeyBoardFocusProperty = DependencyProperty.Register("KeyBoardFocus", typeof(bool), typeof(PasswordBoxBehavior));
            LenghPasswordProperty = DependencyProperty.Register("LenghPassword", typeof(int?), typeof(PasswordBoxBehavior));
            SecurePasswordProperty = DependencyProperty.Register("SecurePassword", typeof(string), typeof(PasswordBoxBehavior));
        }

        public string SecurePassword
        {
            get { return (string)GetValue(SecurePasswordProperty); }
            set { SetValue(SecurePasswordProperty, value); }
        }

        public int? LenghPassword
        {
            get { return (int?)GetValue(LenghPasswordProperty); }
            set { SetValue(LenghPasswordProperty, value); }
        }

        public bool KeyBoardFocus
        {
            get { return (bool)GetValue(KeyBoardFocusProperty); }
            set { SetValue(KeyBoardFocusProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            // Присоединение обработчиков событий            
            this.AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
            this.AssociatedObject.IsKeyboardFocusWithinChanged += AssociatedObject_IsKeyboardFocusWithinChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            // Удаление обработчиков событий
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_PasswordChanged;
            this.AssociatedObject.IsKeyboardFocusWithinChanged -= AssociatedObject_IsKeyboardFocusWithinChanged;
        }

        private void AssociatedObject_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            KeyBoardFocus = this.AssociatedObject.IsKeyboardFocusWithin;
        }

        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            SecurePassword = CalculateSecurePassword(AssociatedObject.Password);
            LenghPassword = CalculateLenghPassword(AssociatedObject.Password);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        private int? CalculateLenghPassword(string CurrentPassword)
        {
            if (CurrentPassword is null)
                return null;
            return CurrentPassword.Length;
        }

        private string CalculateSecurePassword(string CurrentPassword)
        {
            using (SHA256 hash = SHA256.Create())
            {
                return Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(CurrentPassword)));
            }
        }

    }
}
