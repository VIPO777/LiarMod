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

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]