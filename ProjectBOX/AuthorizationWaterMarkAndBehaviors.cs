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

    public class PasswordBoxBehaviorForLogin : PasswordBoxBehavior
    {
        public static readonly DependencyProperty SecurePasswordProperty;

        static PasswordBoxBehaviorForLogin()
        {
            SecurePasswordProperty = DependencyProperty.Register("SecurePassword", typeof(string), typeof(PasswordBoxBehaviorForLogin));
        }

        public string SecurePassword
        {
            get { return (string)GetValue(SecurePasswordProperty); }
            set { SetValue(SecurePasswordProperty, value); }
        }

        protected override void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            base.AssociatedObject_PasswordChanged(sender, e);
            SecurePassword = CalculateSecurePassword(AssociatedObject.Password);
        }

        protected string CalculateSecurePassword(string CurrentPassword)
        {
            using (SHA256 hash = SHA256.Create())
            {
                return Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(CurrentPassword)));
            }
        }
    }

    public class PasswordBoxBehaviorForRegistration : PasswordBoxBehavior
    {
        public static readonly DependencyProperty UserPasswordForRegistrationProperty;
        private bool _skipUpdate;

        static PasswordBoxBehaviorForRegistration()
        {
            UserPasswordForRegistrationProperty = DependencyProperty.Register("UserPasswordForRegistration", typeof(string), typeof(PasswordBoxBehaviorForRegistration));
        }

        public string UserPasswordForRegistration
        {
            get { return (string)GetValue(UserPasswordForRegistrationProperty); }
            set { SetValue(UserPasswordForRegistrationProperty, value); }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == UserPasswordForRegistrationProperty)
            {
                if (!_skipUpdate)
                {
                    _skipUpdate = true;
                    AssociatedObject.Password = e.NewValue as string;
                    _skipUpdate = false;
                }
            }
        }

        protected override void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            base.AssociatedObject_PasswordChanged(sender, e);
            _skipUpdate = true;
            UserPasswordForRegistration = AssociatedObject.Password;
            _skipUpdate = false;
        }
    }

    public class PasswordBoxBehavior : Behavior<PasswordBox>
    {
        public static readonly DependencyProperty LenghPasswordProperty;
        public static readonly DependencyProperty KeyBoardFocusProperty;

        static PasswordBoxBehavior()
        {
            KeyBoardFocusProperty = DependencyProperty.Register("KeyBoardFocus", typeof(bool), typeof(PasswordBoxBehavior));
            LenghPasswordProperty = DependencyProperty.Register("LenghPassword", typeof(int?), typeof(PasswordBoxBehavior));
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

        protected void AssociatedObject_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            KeyBoardFocus = this.AssociatedObject.IsKeyboardFocusWithin;
        }

        protected virtual void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            
            LenghPassword = CalculateLenghPassword(AssociatedObject.Password);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        protected int? CalculateLenghPassword(string CurrentPassword)
        {
            if (CurrentPassword is null)
                return null;
            return CurrentPassword.Length;
        }

    }
}
