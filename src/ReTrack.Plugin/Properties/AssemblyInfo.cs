using System.Reflection;
using JetBrains.ActionManagement;
using JetBrains.Application.PluginSupport;

[assembly: AssemblyTitle("ReTrack")]
[assembly: AssemblyDescription("YouTrack support for ReSharper")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("JetBrains s.r.o")]
[assembly: AssemblyProduct("ReTrack " + Versions.ReTrackVersion)]
[assembly: AssemblyCopyright("Copyright © JetBrains s.r.o, 2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion(Versions.ReTrackVersion)]
[assembly: AssemblyFileVersion(Versions.ReTrackVersion)]
[assembly: ActionsXml("ReTrack.Actions.xml")]

[assembly: PluginTitle("ReTrack")]
[assembly: PluginDescription("YouTrack support for ReSharper")]
[assembly: PluginVendor("JetBrains s.r.o")]

internal static class Versions
{
  public const string ReTrackVersion = "0.1";
}