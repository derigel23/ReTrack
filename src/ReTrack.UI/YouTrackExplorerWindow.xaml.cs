using System.Windows;

namespace ReTrack
{
  using System.Windows.Controls;

  /// <summary>
  /// Interaction logic for YouTrackExplorerWindow.xaml
  /// </summary>
  public partial class YouTrackExplorerWindow : UserControl
  {
    public static readonly DependencyProperty InputTextProperty = DependencyProperty.Register(
      "InputText", typeof (string), typeof (YouTrackExplorerWindow), new PropertyMetadata(default(string)));

    public YouTrackExplorerWindow()
    {
      InitializeComponent();
    }

    public string InputText
    {
      get { return (string) GetValue(InputTextProperty); }
      set { SetValue(InputTextProperty, value); }
    }

    private void TbInput_OnPopulating(object sender, PopulatingEventArgs e)
    {
      
    }
  }
}
