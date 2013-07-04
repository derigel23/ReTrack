using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReTrack.Standalone.Properties;

namespace ReTrack.Standalone.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            var retrackApp = (App)Application.Current;
            UsernameBox.Text = retrackApp.Settings.YouTrackUsername;
            PasswordBox.Password = retrackApp.Settings.YouTrackPassword;
            UrlBox.Text = retrackApp.Settings.YouTrackUrl;
        }

        private void UrlBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UrlBox.Text.ToLowerInvariant().Contains(".myjetbrains.com") &&
                !(UrlBox.Text.ToLowerInvariant().EndsWith("/youtrack") ||
                  UrlBox.Text.ToLowerInvariant().EndsWith("/youtrack/")))
            {
                UrlBox.Text += "/youtrack";
            }
        }

        private void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new YouTrackProxy(new ReTrackSettings
                {
                    YouTrackUsername = UsernameBox.Text,
                    YouTrackPassword = PasswordBox.Password,
                    YouTrackUrl = UrlBox.Text
                });

                MessageBox.Show(
                    string.Format("Successfully connected to {0}.", UrlBox.Text),
                    "Test Connection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLowerInvariant() == "not found" || ex is WebException)
                {
                    MessageBox.Show(
                        "An error occured while connecting to the remote server. Verify the URL is correct and try again.",
                        "Test Connection", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex is AuthenticationException)
                {
                    MessageBox.Show(
                        "An error occured while authenticating with the remote server. Verify your credentials and try again.",
                        "Test Connection", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(
                        "An unspecified error occured while connecting to the remote server.",
                        "Test Connection", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            var retrackApp = (App)Application.Current;
            retrackApp.Settings.YouTrackUsername = UsernameBox.Text;
            retrackApp.Settings.YouTrackPassword = PasswordBox.Password;
            retrackApp.Settings.YouTrackUrl = UrlBox.Text;
        }
    }
}