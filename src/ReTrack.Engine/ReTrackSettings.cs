namespace ReTrack
{
  public class ReTrackSettings
  {
    public string YouTrackUsername { get; set; }
    public string YouTrackPassword { get; set; }
    public string YouTrackHost { get; set; }
    public int YouTrackPort { get; set; }
    public string YouTrackPath { get; set; }
    public bool YouTrackUseSsl { get; set; }

    public ReTrackSettings()
    {
      YouTrackPort = 80;
    }
  }
}