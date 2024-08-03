﻿using System.Reflection;

[assembly: AssemblyTitle(WeatherElectric.VoidSpeaker.Main.Description)]
[assembly: AssemblyDescription(WeatherElectric.VoidSpeaker.Main.Description)]
[assembly: AssemblyCompany(WeatherElectric.VoidSpeaker.Main.Company)]
[assembly: AssemblyProduct(WeatherElectric.VoidSpeaker.Main.Name)]
[assembly: AssemblyCopyright("Developed by " + WeatherElectric.VoidSpeaker.Main.Author)]
[assembly: AssemblyTrademark(WeatherElectric.VoidSpeaker.Main.Company)]
[assembly: AssemblyVersion(WeatherElectric.VoidSpeaker.Main.Version)]
[assembly: AssemblyFileVersion(WeatherElectric.VoidSpeaker.Main.Version)]
[assembly:
    MelonInfo(typeof(WeatherElectric.VoidSpeaker.Main), WeatherElectric.VoidSpeaker.Main.Name,
        WeatherElectric.VoidSpeaker.Main.Version,
        WeatherElectric.VoidSpeaker.Main.Author, WeatherElectric.VoidSpeaker.Main.DownloadLink)]
[assembly: MelonColor(255, 0, 139, 139)]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Stress Level Zero", "BONELAB")]