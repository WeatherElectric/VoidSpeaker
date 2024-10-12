# VoidSpeaker
The successor to Media Player, VoidSpeaker is an immensely improved version.

Less bugs, better UI.

# What's Different From Media Player?
* Uses Bonemenu rather than a physical object
* No more spatial audio, it was annoying
* Refreshing files in runtime, if you add a new song to the folder and hit "Refresh", it will add it
* Heavily improved performance with TagLib. Trust me, it sucked before. It wouldn't destroy Texture2Ds when no longer used. Memory leaks.
* Can be shuffled in runtime
* Can be paused
* MP3 files do not need to be edited anymore, it resizes their images itself now, way easier to use
* SDK script that allows you to get the current song's metadata

# IMPORTANT!
* DEPENDING ON HOW MANY MP3 FILES YOU PUT IN THE FOLDER, THE GAME MAY TAKE LONGER TO START UP!
* THIS IS INEVITABLE! PUTTING IT ON ANOTHER THREAD CRASHES THE GAME, AND IF IT WERE ASYNC IT MAY NOT BE FULLY LOADED BY THE TIME YOU SPAWN ONE IN, CAUSING PROBLEMS!
* THERE WILL ALSO BE A SLIGHT LAG SPIKE WHEN LOADING INTO THE MAIN MENU! THIS ONLY HAPPENS ONCE, NEVER AGAIN!

# Setup
## Mod
### Installation
* Place `TagLibSharp.dll` in the UserLibs folder
* Place `VoidSpeaker.dll` in the Mods folder and run the game once
### Preferences
* In `WeatherElectric.cfg`, all preferences for the mod are in the VoidSpeaker category
* You can also edit them in game through BoneMenu.
## Custom Audio
### Installation
* Place your MP3 files into `UserData/Weather Electric/Void Speaker`
## Unity Integration
* Import `VoidSpeaker.unitypackage` into your project
* Add `MetadataListener` to an object
* Example Setup:
![Example Setup](https://raw.githubusercontent.com/WeatherElectric/VoidSpeaker/main/Assets/ListenerSetup.png)