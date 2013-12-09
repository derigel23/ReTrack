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
    private string _summary;
    private string _state;
    private string _type;
    private string _id;
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
      get { return _id; }
      set
      {
        if (value == _id) return;
        _id = value;
        OnPropertyChanged("Id");
      }
    }

    public string Type
    {
      get { return _type; }
      set
      {
        if (value == _type) return;
        _type = value;
        OnPropertyChanged("Type");
      }
    }

    public string State
    {
      get { return _state; }
      set
      {
        if (value == _state) return;
        _state = value;
        OnPropertyChanged("State");
      }
    }

    public string Summary
    {
      get { return _summary; }
      set
      {
        if (value == _summary) return;
        _summary = value;
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
      Url = proxy.BaseUrl + '/' + issue.ID;

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
              Application.Current.Dispatcher.Invoke(new Action(() => Comments.Add(comment)));
            }
          });
    }
  }
}