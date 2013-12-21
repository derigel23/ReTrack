namespace ReTrack.UI.Views.Issue
{
  using System;
  using System.Collections.ObjectModel;
  using System.Threading.Tasks;
  using System.Windows;
  using Engine;
  using Engine.Models;
  using ReTrack.UI.Infrastructure;

  public class IssueViewModel : ViewModelBase
  {
    private string summary;
    private string state;
    private string type;
    private string id;
    private string url;

    public string Url
    {
      get { return url; }
      set
      {
        if (value.Equals(url)) return;
        url = value;
        OnPropertyChanged("Url");
      }
    }

    public YouTrackProxy Proxy { get; set; }

    public ObservableCollection<ShortComment> Comments { get; set; }

    public string Id
    {
      get { return id; }
      set
      {
        if (value == id) return;
        id = value;
        OnPropertyChanged("Id");
      }
    }

    public string Type
    {
      get { return type; }
      set
      {
        if (value == type) return;
        type = value;
        OnPropertyChanged("Type");
      }
    }

    public string State
    {
      get { return state; }
      set
      {
        if (value == state) return;
        state = value;
        OnPropertyChanged("State");
      }
    }

    public string Summary
    {
      get { return summary; }
      set
      {
        if (value == summary) return;
        summary = value;
        OnPropertyChanged("Summary");
      }
    }

    public IssueViewModel()
    {
      Comments = new ObservableCollection<ShortComment>();
    }

    public void Initialize(YouTrackProxy proxy, ShortIssue issue)
    {
      Proxy = proxy;

      Id = issue.ID;
      Type = issue.Type;
      State = issue.State;
      Summary = issue.Summary;
      Url = proxy.BaseUrl + "/issue/" + issue.ID;

      QueryForComments(issue.ID);
    }

    public void SubmitComment(string text)
    {
      CommentProcessor.Process(text, Proxy, Id);
//      Task.Factory.StartNew(() => Proxy.SubmitComment(Id, text))
//        .ContinueWith(r => QueryForComments(Id));
    }

    public void QueryForComments(string issueId)
    {
      Task.Factory.StartNew(() => Proxy.QueryComments(issueId)).ContinueWith(
          r =>
          {
            Application.Current.Dispatcher.Invoke(new Action(() => Comments.Clear()));
            foreach (var comment in r.Result)
            {
              ShortComment comment1 = comment;
              Application.Current.Dispatcher.Invoke(new Action(() => Comments.Add(comment1)));
            }
          });
    }
  }
}