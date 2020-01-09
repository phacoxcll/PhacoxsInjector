# Phacox's Injector
This is a program that allows you to inject games into the Virtual Console of the Wii U. It is compatible with NES, SNES, N64, GBA and NDS games.

Characteristics

- Easy to use graphic interface.
- Contextual help and two languages, English and Spanish.
- Automatically recognize ROM formats, *.nes, *.fds, *.sfc, *.smc, *.z64, *.n64, *.v64, *.gba and *.nds
- Support images *.png, *.jpg and *.bmp
- Configuration of the N64 Virtual Console, easily disables the dark filter, aspect ratio and display scale of the game. It simplifies the incorporation of the ".ini" configuration file for the game and has an advanced editor "VCN64ConfigEditor".
- The Title ID reflects whether you have used the same combination of ROM and base game.
- Option to package the result (WUP Installer format) or leave it unpacked (Loadiine format).
- It is able to remember the folders of your ROM collections and image gallery.
- You can use any game as a base (you can only inject the ROM if the base supports it, although this does not guarantee that the game works).
- Use Wii U Virtual Console games packed (files, title.cert, title.tik, title.tmd, "*.app" and "*.h3") or unpacked (folders code, content and meta) as base.

## Usage

### Mode selection

Auto
- The program adapts automatically according to the chosen ROM.
- Enable the base game load after choosing the ROM.
- In this mode you will use the image templates of NES, SNES (USA), N64, GBA and NDS, as appropriate.

Famicom
- You can only choose ROMs for Famicom and NES.
- Enable the base game load for Famicom and NES.
- Use Famicom image templates.

NES
- You can only choose ROMs for Famicom and NES.
- Enable the base game load for Famicom and NES.
- Use the NES image templates.

Super Famicom
- You can only choose ROMs for Super Famicom and SNES.
- Enable the base game load for Super Famicom and SNES.
- Use Super Famicom image templates.

SNES (EUR)
- You can only choose ROMs for Super Famicom and SNES.
- Enable the base game load for Super Famicom and SNES.
- Use the SNES (EUR) image templates.

SNES (USA)
- You can only choose ROMs for Super Famicom and SNES.
- Enable the base game load for Super Famicom and SNES.
- Use the SNES (USA) image templates.

N64
- You can only choose ROMs for N64.
- Enable the base game load for N64.
- Use the N64 image templates.

GBA
- You can only choose ROMs for GBA.
- Enable the base game load for GBA.
- Use the GBA image templates.

NDS
- You can only choose ROMs for NDS.
- Enable the base game load for NDS.
- Use the NDS image templates.


### Main

In this section you just need to put the name of the game and load a base (you can use the same base that is already loaded). Choosing a ROM is optional!

If you choose a ROM, the program will change the name that appears in the console menus to the new name you set and will inject the selected ROM.

If you do not choose a ROM, the program will not change the name of the base game, the new name you entered will only be used to name the output folder and the base game ROM will not be modified. Useful if you just want to edit some option of the Virtual Console without modifying anything else.

You can change the Virtual Console settings.

For Famicom and NES
- Change the aspect ratio, 7:5 (default and not recommended), 8:7 (native, recommended for square pixels), 4:3 (used in old square televisions) and 16:9 (widescreen, to occupy the full width of modern televisions).

For Super Famicom and SNES
- Change the aspect ratio, 4:3 (default, it was used in old square televisions), 8:7 (native, recommended to obtain square pixels) and 16:9 (wide screen, to occupy the full width of the modern televisions).

For Nintendo 64
- Disable the dark filter.
- Change the aspect ratio from 4:3 to 16:9 (this stretches the image if the ROM does not have a widescreen patch)
- Change the display scale of the game.
- Choose the configuration file that corresponds to the game you are going to inject (not all games have a configuration file and partly because of that they do not work in the Virtual Console).


### Images

In this section you can modify the menu icon and the game presentation images.

You can load only the image of the title screen and it will be updated in all templates, Select the year of launch of the game, the number of players it supports and if you want to show or hide the name you gave the game.

You also have the option to keep the images of the base game. Useful if you just want to edit some option of the Virtual Console without modifying anything else.

For Nintendo DS, you can use the ROM icon instead of the title screen image.


### Injecting

To enable the buttons, name the game and load a base. Injecting a ROM is optional (see the Main section).

By clicking on the "Do pack" button you will have to select a folder where the game will be placed in WUP Installer format (files, title.cert, title.tik, title.tmd, "*.app" and "*.h3"). If you have not yet set the Wii U Common Key, it will result in a failed injection (technically the injection is performed, but the result cannot be packed).

By clicking on the "Do not pack" button you will have to select a folder where the game will be placed in Loadiine format (folders, code, content and meta).

Regardless of whether it fails or not, an injection will show the program log file.


### Settings

In this section you can enter the Wii U Common Key, this will allow you to package and unpack games in WUP format.

You can change the language of the program and disable contextual help.



## Changelog
1.0.6 (2019-12-29)
- Improves validation of N64 ROMs.
- Improves NES aspect ratios.

1.0.5 (2019-12-27)
- Update the version of CNUSPacker.
- Some minor fixes.

1.0.4 (2019-12-27)
- Fix a bug where the short name was always used as a long name.
- Fix a bug where the name was not drawn on the template images if it was not previously passed through the Images section.

1.0.3 (2019-12-26)
- Fix an error in the way of transferring the common key to CNUSPacker.

1.0.2 (2019-12-25)
- Fix an error when loading encrypted base games, the load was always failed due to a resource management error.

1.0.1 (2019-12-20)
- Replaces the use of NUSPacker.jar in favor of CNUSPacker.exe.

1.0 (2019-12-19)
- Initial release.



## Disclaimer

The tools packaged with this application belong to their respective developers.

Credits

CNUSPacker by NicoAICP, Morilli - https://github.com/Hotbrawl20/CNUS_Packer

CDecrypt v3.0 by Crediar, phacox.cll - https://github.com/phacoxcll/cdecrypt

inject_gba_C by Morilli - https://github.com/Morilli/inject_gba_C
