using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LoginApp
{
    public partial class LoginControl : UserControl
    {
        public static readonly RoutedCommand LoginCommand = new RoutedCommand();

        public LoginControl()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(LoginCommand, OnLoginExecuted));
        }

        private void OnLoginExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageText.Text = "Будь ласка, заповніть усі поля.";
                MessageText.Foreground = Brushes.Red;
            }
            else
            {
                MessageText.Text = "Вхід успішний!";
                MessageText.Foreground = Brushes.Green;
            }
        }
    }
}
