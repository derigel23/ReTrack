namespace ReTrack.UI.Views.Issue
{
  using System.Diagnostics;
  using System.Windows.Controls;
  using System.Windows.Input;
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

      private void CommentBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
      {
        if (e.Key == System.Windows.Input.Key.Enter &&
          Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
        {
          // submit the comment
          var tb = (TextBox)sender;
          ViewModel.SubmitComment(tb.Text);
          tb.Text = string.Empty;
        }
      }
    }
}
