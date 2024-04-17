# VoidSpeaker
A music player for BONELAB

# IMPORTANT!
* DEPENDING ON HOW MANY MP3 FILES YOU PUT IN THE FOLDER, THE GAME MAY TAKE LONGER TO START UP!
* THIS IS INEVITABLE! PUTTING IT ON ANOTHER THREAD CRASHES THE GAME, AND IF IT WERE ASYNC IT MAY NOT BE FULLY LOADED BY THE TIME YOU SPAWN ONE IN, CAUSING PROBLEMS!

# Setup
## Mod
### Installation
* Place TagLibSharp.dll in the UserLibs folder
* Place VoidSpeaker.dll in the Mods folder and run the game once
### Preferences
* In WeatherElectric.cfg, all preferences for the mod are in the VoidSpeaker category
* You can also edit them in game through BoneMenu.
## Custom Audio
### Requirements
* MP3 files
* [Mp3Tag](https://www.mp3tag.de/en/)
### File Setup
* Drag your MP3 files into Mp3Tag
* Select all of them
  ![image](https://github.com/WeatherElectric/MediaPlayer/assets/30084485/ccbcff5c-02ab-41cf-90d1-0e73f637f6a0)
* Right click and hit "Actions"
* Create a new action and name it anything
* Add an "Adjust cover" action to it
  ![image](https://github.com/WeatherElectric/MediaPlayer/assets/30084485/e41c2489-dfef-4c3e-adb8-a148e5085100)
* Set the max size to "336"
* Deselect all action groups EXCEPT the one you just made
  ![image](https://github.com/WeatherElectric/MediaPlayer/assets/30084485/1f8ed712-3326-462d-945a-d9df1ffd93f6)
* Hit "OK"
### Installation
* Place your MP3 files into UserData/Weather Electric/Void Speaker