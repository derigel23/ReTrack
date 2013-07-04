using System;
using System.Net;
using System.Security.Authentication;
using System.Windows;
using System.Windows.Controls;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Features.Common.Options;
using JetBrains.UI.CrossFramework;
using JetBrains.UI.Options;
using JetBrains.UI.Settings;
using JetBrains.Application.Settings;

namespace ReTrack
{
    [OptionsPage(PID, "ReTrack",
        typeof (IconsForDefaultSettingsStorages),
        ParentId = ToolsPage.PID)]
    public partial class ReTrackOptionsPage : UserControl, IOptionsPage
    {
        private const string PID = "ReTrack.OptionsPage";

        public ReTrackOptionsPage(Lifetime lifetime, OptionsSettingsSmartContext ctx)
        {
            InitializeComponent();

            ctx.SetBinding(lifetime, (ReTrackSettingsReSharper s) => s.YouTrackUsername, UsernameBox, TextBox.TextProperty);
            ctx.SetBinding(lifetime, (ReTrackSettingsReSharper s) => s.YouTrackPassword, PasswordBox, TextBox.TextProperty);
            ctx.SetBinding(lifetime, (ReTrackSettingsReSharper s) => s.YouTrackUrl, UrlBox, TextBox.TextProperty);
        }

        public bool OnOk()
        {
            return true;
        }

        public bool ValidatePage()
        {
            // todo: attempt to connect to source (if specified)
            return true;
        }

        public EitherControl Control
        {
            get { return this; }
        }

        public string Id
        {
            get { return PID; }
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
    }
}