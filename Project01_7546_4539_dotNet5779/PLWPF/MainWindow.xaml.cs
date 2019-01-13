using BL;
using BE;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                GoogleAPIFunc a = new GoogleAPIFunc();
                IBL bl = Bl_imp.GetBl();
               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void Trainee_Click(object sender, RoutedEventArgs e)
        {
             try
            {
            //this.Hide();
            LogInWindow logInWindow = new LogInWindow(this,"trainee");
            logInWindow.ShowDialog();
                //  this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Tester_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //this.Hide();
                LogInWindow logInWindow = new LogInWindow(this, "tester");
                logInWindow.ShowDialog();
                //  this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Manager_OnClick(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            LogInWindow logInWindow = new LogInWindow(this, "manager");
            logInWindow.ShowDialog();
            //  this.Close();

        }
    }



    public class SecurePassword
    {

        private static readonly DependencyProperty PasswordInitializedProperty =
            DependencyProperty.RegisterAttached("PasswordInitialized", typeof(bool), typeof(SecurePassword), new PropertyMetadata(false));

        private static readonly DependencyProperty SettingPasswordProperty =
            DependencyProperty.RegisterAttached("SettingPassword", typeof(bool), typeof(SecurePassword), new PropertyMetadata(false));

        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }
        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }
        // We play a trick here. If we set the initial value to something, it'll be set to something else when the binding kicks in,
        // and HandleBoundPasswordChanged will be called, which allows us to set up our event subscription.
        // If the binding sets us to a value which we already are, then this doesn't happen. Therefore start with a value that's
        // definitely unique.
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(SecurePassword),
                new FrameworkPropertyMetadata(Guid.NewGuid().ToString(), HandleBoundPasswordChanged)
                {
                    BindsTwoWayByDefault = true,
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.LostFocus // Match the default on Binding
                });

        private static void HandleBoundPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = dp as PasswordBox;
            if (passwordBox == null)
                return;

            // If we're being called because we set the value of the property we're bound to (from inside 
            // HandlePasswordChanged, then do nothing - we already have the latest value).
            if ((bool)passwordBox.GetValue(SettingPasswordProperty))
                return;

            // If this is the initial set (see the comment on PasswordProperty), set ourselves up
            if (!(bool)passwordBox.GetValue(PasswordInitializedProperty))
            {
                passwordBox.SetValue(PasswordInitializedProperty, true);
                passwordBox.PasswordChanged += HandlePasswordChanged;
            }

            passwordBox.Password = e.NewValue as string;
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            passwordBox.SetValue(SettingPasswordProperty, true);
            SetPassword(passwordBox, passwordBox.Password);
            passwordBox.SetValue(SettingPasswordProperty, false);
        }
    }
}
