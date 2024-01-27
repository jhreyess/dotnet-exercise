using System;
using System.Windows;

namespace PokeApi.views
{
    public partial class EmailDialog : Window
    {
        public string EnteredEmail { get; private set; }
        public EmailDialog()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            EnteredEmail = EmailTextBox.Text;
            DialogResult = true;
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void EmailTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            YesButton.IsEnabled = !string.IsNullOrEmpty(EmailTextBox.Text);
        }
    }
}
