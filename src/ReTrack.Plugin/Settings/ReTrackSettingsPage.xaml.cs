using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JetBrains.Application.Settings;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Features.Common.Options;
using JetBrains.UI.CrossFramework;
using JetBrains.UI.Options;
using ReTrack.Engine;
using ReTrack.Resources;

namespace ReTrack.Settings
{
  [OptionsPage(PID, "ReTrack", typeof(ReTrackThemedIcons.YouTrack), ParentId = ToolsPage.PID)]
  public partial class ReTrackOptionsPage : UserControl, IOptionsPage
  {
    private const string PID = "ReTrack.OptionsPage";

    public ReTrackOptionsPage(Lifetime lifetime, OptionsSettingsSmartContext ctx)
    {
      InitializeComponent();
      
      ctx.SetBinding(lifetime, (ReTrackSettingsReSharper s) => s.YouTrackUsername, UsernameBox, TextBox.TextProperty);
      
      // it is entirely uncertain how to store the password

      ctx.SetBinding(lifetime, (ReTrackSettingsReSharper s) => s.YouTrackUrl, UrlBox, TextBox.TextProperty);
      ctx.SetBinding(lifetime, (ReTrackSettingsReSharper s) => s.Port, PortBox, TextBox.TextProperty);
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
      if (UrlBox.Text.IndexOf(".myjetbrains.com", StringComparison.OrdinalIgnoreCase) >= 0 &&
          !(UrlBox.Text.EndsWith("/youtrack", StringComparison.OrdinalIgnoreCase) ||
            UrlBox.Text.EndsWith("/youtrack/", StringComparison.OrdinalIgnoreCase)))
      {
        UrlBox.Text += "/youtrack";
      }
    }

    private void TestConnection_Click(object sender, RoutedEventArgs e)
    {
      var validator = new YouTrackConnectionValidator();
      var errorMessage = "";
      if (!validator.TryValidateConnection(new ReTrackSettings
      {
        Username = UsernameBox.Text,
        Password = PasswordBox.Password,
        Url = UrlBox.Text
      }, out errorMessage))
      {
        MessageBox.Show(
            errorMessage,
            "Test Connection", MessageBoxButton.OK, MessageBoxImage.Error);
      }
      else
      {
        MessageBox.Show(
            string.Format("Successfully connected to {0}.", UrlBox.Text),
            "Test Connection", MessageBoxButton.OK, MessageBoxImage.Information);
      }
    }

    public bool IsPortValueAllowed(string text)
    {
      byte s;
      return byte.TryParse(text, out s);
    }

    /// <summary>
    /// Prevent entry of non-numeric data.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PortBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
      e.Handled = !IsPortValueAllowed(e.Text);
    }

    /// <summary>
    /// Prevent pasting of non-numeric data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PortBox_OnPasting(object sender, DataObjectPastingEventArgs e)
    {
      if (e.DataObject.GetDataPresent(typeof(String)))
      {
        var text = (String)e.DataObject.GetData(typeof(String));
        if (!IsPortValueAllowed(text))
        {
          e.CancelCommand();
        }
      }
      else
      {
        e.CancelCommand();
      }
    }

    private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
    {
      
    }
  }
}