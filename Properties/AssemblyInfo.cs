using MelonLoader;
using System.Reflection;

[assembly: AssemblyTitle(LiarMod.BuildInfo.Description)]
[assembly: AssemblyDescription(LiarMod.BuildInfo.Description)]
[assembly: AssemblyCompany(LiarMod.BuildInfo.Company)]
[assembly: AssemblyProduct(LiarMod.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + LiarMod.BuildInfo.Author)]
[assembly: AssemblyTrademark(LiarMod.BuildInfo.Company)]
[assembly: AssemblyVersion(LiarMod.BuildInfo.Version)]
[assembly: AssemblyFileVersion(LiarMod.BuildInfo.Version)]
[assembly: MelonInfo(typeof(LiarMod.LiarMod), LiarMod.BuildInfo.Name, LiarMod.BuildInfo.Version, LiarMod.BuildInfo.Author, LiarMod.BuildInfo.DownloadLink)]
[assembly: MelonColor()]
[assembly: MelonGame("Curve Animation", "Liar's Bar")]