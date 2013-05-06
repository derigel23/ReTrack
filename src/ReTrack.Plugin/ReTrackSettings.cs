using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Application.Communication;
using JetBrains.Application.Settings;

namespace ReTrack
{
  [SettingsKey(typeof(InternetSettings), "ReTrack Settings")]
  public class ReTrackSettings
  {
    [SettingsEntry("", "YouTrack Username")]
    public string YouTrackUsername { get; set; }

    [SettingsEntry("", "YouTrack Password")]
    public string YouTrackPassword { get; set; }

    [SettingsEntry("", "YouTrack Host")]
    public string YouTrackHost { get; set; }

    [SettingsEntry(false, "YouTrack Use SSL")]
    public bool YouTrackUseSsl { get; set; }
  }
}
