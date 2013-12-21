using System.Windows.Controls;
using System.Windows.Input;
using ReTrack.UI.Infrastructure;

namespace ReTrack.UI.Views.IssueBrowser
{
    public partial class IssueBrowserView 
        : UserControl, IViewModelBoundElement<IssueBrowserViewModel>
    {
        public IssueBrowserView()
        {
            InitializeComponent();
        }

        public IssueBrowserViewModel ViewModel
        {
            get { return DataContext as IssueBrowserViewModel; }
            private set { DataContext = value; }
        }

      private void SearchBox_OnKeyDown(object sender, KeyEventArgs e)
      {
        if (e.Key == Key.Enter)
        {
          ViewModel.QueryForIssues();
        }
      }
    }
}