# iTunes Skin Tools
--------------
![Sample Img](https://github.com/Apophenic/iTunes-Skin-Tools/blob/master/iTunesSkinTools/res/sample/sample.jpg)
iTunes Skin Tools makes creating skins / themes for iTunes in Windows far more seamless and accessible by automating
the process of injecting and extracting resource files into iTunes.dll.

### How It Came To Be
---------------------
Have your eyes grown weary and fatigued from this incessant trend of acute black text on exceedindly high contrast white
backgrounds that perpetuates in all facets of contemporary software design resulting in sterile user interfaces? Was
 that an overly drawn out sentence?
If you answered yes to both questions, read on.

### About iTunes.dll
----------------
iTunes\iTunes.Resources\iTunes.dll is a custom assembly file created by Apple for use in iTunes. It's a resource-only
 .dll file, meaning it contains primarily resources such as images as opposed to actual code. We're interested in the
 RCData, which is a collection of raw _.png_ files that are used for iTunes' user interface. Each file has a unique
 unsigned integer ID. You can view the dll structure (and manipulate it) using [Resource Hacker](http://www.angusj.com/resourcehacker/#download)
 or with Visual Studio (dotPeek can't parse it).

### iTunes.dll RCData IDs
-------------------------
These are the IDs and how they are used in the UI:
* 150 / 168 - Now Playing background
* 151 - Apple logo in Now Playing box
* 160 / 163 / 2109 - Volume slider color, other is likely for now playing time slider
* 161 - Most likely tick mark for now playing time slider
* 304, 305 - Song Column Header
* 317, 319 - Top Control bar skip buttons
* 321 - Top Control Play/Pause/Stop buttons
* 350 - Add to playlist header (?)
* 732-735 - One of these is edit/Add to button in playlists
* 736, 741-743 / Get info button colors
* 803 / 804 - Menu bar (below now playing bar)
* 816 / 817 - Menu bar button + text colors
* 3506/13/14/26/33/34/66/73/74 - Scroll bar backgrounds
* 3525 - Scroll bar foreground
* 5000 - Top control bar
* 5001 - Bottom Info Bar
* 5002 - ?
* 8100 / 01 - Mini play skin
* 8102 / 03 - Mini player background (?)
* 8441 - Playlist creation background (bottom left)

I'll update this list as I discover how other files are used.

### What iTunes Skin Tools Does
---------------------------
iTunes Skin Tools currently supports extracting all image resource files from iTunes.dll as well as injecting resources
back into the .dll file. Extracted files are named after their corresponding resource ID (i.e. _5000_ will be
extracted as _5000.png_)In a typical use case, you'll extract all resources, edit IDs of your choosing, then inject
the modified files back into iTunes.dll.

### How To Use It
-----------------
```iTunesSkinTools.exe -op=extract||inject -itunesdir="C:\Program Files\iTunes" -workingdir="C:\Directory"```
* _op_: __Extract__ will extract all files from iTunes.dll into _workingdir_, while __Inject__ will inject all
files from _workingdir_ into iTunes.dll.
* _itunesdir_: iTune.exe's parent directory

__Currently this is only tested on the latest version of iTunes x64 (12.1.2.27). This will almost certainly not work
for x32 versions of iTunes (for now).__

### Project Status
------------------
Currently supports:
* iTunes resource injection for custom skins and themes
* iTunes resource extraction

Planned:
* An exhaustive guide (w/ images) for resource IDs and the component they effect
* Support for changing text color (unfortunately, this may be impossible)
* Support for previous (and future) versions of iTunes
* Support for x32 iTunes, if necessary
* Support for other files containing resources, such as iTunes.exe (icons)