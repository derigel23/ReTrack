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
        public YouTrackProxy Proxy { get; set; }

        public YouTrackExplorerWindow()
        {
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
                var owner = (AutoCompleteBox) sender;
                var s = owner.Text;
            }
        }
    }
}