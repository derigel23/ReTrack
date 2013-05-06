using System.Windows;
using System.Windows.Input;

namespace ReTrack
{
  using System.Windows.Controls;

  /// <summary>
  /// Interaction logic for YouTrackExplorerWindow.xaml
  /// </summary>
  public partial class YouTrackExplorerWindow : UserControl
  {
    private readonly YouTrackProxy proxy;

    public YouTrackExplorerWindow()
    {
      this.proxy = proxy;
    }

    private void TbInput_OnPopulating(object sender, PopulatingEventArgs e)
    {
      
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void OnInputBoxKeyUp(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        
      }
    }
  }
}
