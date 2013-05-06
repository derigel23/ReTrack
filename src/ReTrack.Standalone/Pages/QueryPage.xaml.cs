using System;
using System.Windows;
using System.Windows.Controls;

namespace ReTrack.Standalone.Pages
{
  /// <summary>
  /// Interaction logic for QueryPage.xaml
  /// </summary>
  public partial class QueryPage : Page
  {
    public QueryPage()
    {
      InitializeComponent();
    }

    private void BtnSettings_OnClick(object sender, RoutedEventArgs e)
    {
      if (NavigationService != null)
        NavigationService.Navigate(new Uri(@"Pages\SettingsPage.xaml", UriKind.RelativeOrAbsolute));
    }
  }
}
