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
    typeof(IconsForDefaultSettingsStorages),
    ParentId = ToolsPage.PID)]
  public partial class ReTrackOptionsPage : UserControl, IOptionsPage
  {
    private const string PID = "ReTrack.OptionsPage";

    public ReTrackOptionsPage(Lifetime lifetime, OptionsSettingsSmartContext ctx)
    {
      InitializeComponent();

      ctx.SetBinding(lifetime, (ReTrackSettingsReSharper s) => s.YouTrackUsername, usernameBox, TextBox.TextProperty);
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

    public EitherControl Control { get { return this; } }
    public string Id { get { return PID; } }
  }
}
