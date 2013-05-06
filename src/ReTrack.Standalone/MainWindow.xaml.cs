using System.Windows;
using System.Windows.Navigation;

namespace ReTrack.Standalone
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : NavigationWindow
  {
    public ReTrackSettings Settings { get; set; }

    public MainWindow()
    {
      InitializeComponent();
    }
  }
}
