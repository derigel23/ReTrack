namespace ReTrack.Navigation
{
  using JetBrains.ReSharper.Feature.Services.Navigation.Search;
  using JetBrains.ReSharper.Feature.Services.Occurences;
  using JetBrains.ReSharper.Feature.Services.Occurences.Presentation;
  using JetBrains.UI.PopupMenu;

  [OccurencePresenter(Priority=0)]
  public class YouTrackOccurencePresenter : IOccurencePresenter
  {
    public bool Present(IMenuItemDescriptor descriptor, IOccurence occurence,
      OccurencePresentationOptions occurencePresentationOptions)
    {
      var o = ((YouTrackIssueOccurence) occurence);
      descriptor.Text = o.IssueId;
      descriptor.Text.Append(" - ");
      descriptor.Text.Append(o.IssueDescription);
      descriptor.Style = MenuItemStyle.Enabled;

      return true;
    }

    public bool IsApplicable(IOccurence occurence)
    {
      return occurence is YouTrackIssueOccurence;
    }
  }
}