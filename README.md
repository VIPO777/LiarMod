# LiarMod

Some of the mods and patches for **Liar's Bar** were created purely for fun, like mini pigs or other interactions.

## Features

### General Features
- **FreePos**: Allows move their game character freely. Also available in Lobby [Host only]
- **FreeNetObjPost**: Enables the host to select and move network objects from the NetObjects menu in the LiarMenu (accessed by pressing "M"). Some objects may require a respawn (press "R") to update their position and scale.

### LiarsDeck-Only Mods
- **FreeCountDown**: Allows you to bypass the game timer for your turn
- **FreeTurn**: Allows to throw cards out of turn.
- **FreeThrowCard**: Allows to throw more than three cards in the game. *(Not recommended; may cause network exceptions.)*

### Host-Only Features
- **Change Username**: Allows the host to change their username in the Host room.
- **CustomLocalPlayerSize**: Enables the host to change the size/scale of their game model during gameplay or in the lobby.
- **CustomNetPlayerSize**: Allows the host to modify other players' game model sizes/scales during gameplay or in the lobby.
- **MiniPigMod**: 
  - **MiniPigMod**: Transforms the host’s game character into a mini pig.
  - **MiniPigModShare**: Turns all players in the lobby into mini pigs.
  - **MiniPigModPapa**: Sets the host's pig size larger than other players’.
- **FreeButtonActive**: Allows the host to start the game without waiting for other players to be ready.

### NetObjects Menu
- **Players**: Displays players in the game.
- **Net Objects**: Displays network objects.
- **ExploreObject**: Displays child game objects from netobject.

#### Host-Only Commands in NetObjects Menu
- **R**: Respawns selected objects *(not recommended for player objects)*.
- **Ex**: Explores child game objects within a network object.
- **M**: Selects an object for FreeNetObjPost.

---

## Key Controls

- **5** - Open LiarMenu
- **F5** - Enable FreeCam (renders head automatically)
- **F6** - Enable FreePos
- **F8** - Render local player head
- **F9** - Reset FreePos position to default (works if FreePos is active)
- **F10** - Freeze FreeCam movement
- **O** - Open Mouth
- **I** - Enable CrazyShakeHead

### Movement Controls
- **WASD + Mouse** - Move and control camera
- **Space** - Move Up
- **Left Ctrl** - Move Down
- **Q** - Increase scale
- **E** - Decrease scale

---

## Default Patches

- **Skip Game Intro**: Bypass the game intro sequence.
- **Remove Head Rotation Limit**: Removes limitations on head rotation.
- **Chinese Name Fix Patch**: Fixes issues with Chinese character names.

## Installation

1. Download [MelonLoader.Installer](https://github.com/LavaGang/MelonLoader/releases/download/v0.6.5/MelonLoader.Installer.exe).
2. Select **Liar's Bar.exe** in your root game folder and install MelonLoader.
3. Download **LiarMod.dll** from the [Release page](https://github.com/VIPO777/LiarMod/releases).
4. Place **LiarMod.dll** in the `mods` folder inside your root game directory.

## Compiling the Project

To compile the LiarMod project, follow these steps:

1. **Create the Libs Folder**  
   Create a folder named `Libs` in the root of your project.

2. **Copy Assemblies**  
   Copy all assemblies from the following game directory into the `Libs` folder:    
   `Liar's Bar\Liar's Bar_Data\Managed`

3. **Add MelonLoader**  
   Copy `MelonLoader.dll` from the following game directory into the `Libs` folder:  
   `Liar's Bar\MelonLoader\net35`

4. **Restore Packages**  
   Open a terminal or command prompt and navigate to your project directory. Run the following command to restore the NuGet packages:  
   ```bash
   dotnet restore


## Credits
- [LiarsBarEnhance](https://github.com/dogdie233/LiarsBarEnhance) for some patches.
