using JetBrains.Application.Communication;
using JetBrains.Application.Settings;

namespace ReTrack
{
  [SettingsKey(typeof(InternetSettings), "ReTrack Settings")]
  public class ReTrackSettingsReSharper : ReTrackSettings
  {
    [SettingsEntry("", "YouTrack Username")]
    public new string YouTrackUsername
    {
      get { return base.YouTrackUsername; }
      set { base.YouTrackUsername = value; }
    }

    [SettingsEntry("", "YouTrack Password")]
    public new string YouTrackPassword
    {
      get { return base.YouTrackPassword; }
      set { base.YouTrackPassword = value; }
    }

    [SettingsEntry("", "YouTrack Host")]
    public new string YouTrackHost
    {
      get { return base.YouTrackHost; }
      set { base.YouTrackHost = value; }
    }

    [SettingsEntry(false, "YouTrack Use SSL")]
    public new bool YouTrackUseSsl
    {
      get { return base.YouTrackUseSsl; }
      set { base.YouTrackUseSsl = value; }
    }
  }
}
