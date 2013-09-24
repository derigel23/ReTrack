namespace ReTrack.UI.Views.Issue
{
  using System.Diagnostics;
  using System.Windows.Controls;
  using System.Windows.Navigation;
  using Infrastructure;

  public partial class IssueView
        : UserControl, IViewModelBoundElement<IssueViewModel>
    {
        public IssueView()
        {
            InitializeComponent();
        }

        public IssueViewModel ViewModel
        {
            get { return DataContext as IssueViewModel; }
            private set { DataContext = value; }
        }

      private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
      {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
        e.Handled = true;
      }
    }
}
