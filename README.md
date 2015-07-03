# iTunes Skin Tools
--------------
![Sample Img](https://github.com/Apophenic/iTunes-Skin-Tools/blob/master/res/sample/sample.jpg)
iTunes Skin Tools makes creating skins / themes for iTunes in Windows far more seamless and accessible by automating
the process of injecting modified resource files into iTunes.dll.

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
 unsigned integer ID. You can view the dll structure and extract files using [Resource Hacker](http://www.angusj.com/resourcehacker/#download)
 or with Visual Studio. (dotPeek can't parse it)

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
Currently, iTunes Skin Tools only injects resource files into iTunes.dll. You'll need to manually extract whatever
files you wish to edit, then inject them using this tool. It's recommended to use [Resource Hacker](http://www.angusj.com/resourcehacker/#download)
for manual extraction. Simply open iTunes.dll, open the RCData folder, right-click 'save resource as _*.bin_', and
rename the saved file to *.png.

### How To Use It
-----------------
After running the compiled .exe directly, you'll be prompted for your iTunes directory, the directory containing
files to inject, and if you wish to create a backup of your iTunes.dll file. Notes:
* Your iTunes directory is simply iTunes.exe's parent directory
* The files to inject __must__ be named after their corresponding resource ID. If you were replacing ID 5000, the
replacement file must be named '_5000.png_'. Other image types _may_ be supported.
* A backup is created by default, but does not overwrite. It's located at _\iTunes\iTunes.Resources\iTunes.dll.bak_

__Currently this is only tested on iTunes 12.1.2.27 x64. This will almost certainly not work for x32 versions of
iTunes.__

The good news is that, going forward, this should remain a pretty permanent solution (unless Apple completely
overhauls their resources, which hasn't been done since at least iTunes 10').

### Project Status
------------------
Currently supports:
* iTunes resource injection for custom skins and themes

Planned:
* Application to rip resources by ID from iTunes.dll
* An exhaustive guide (w/ images) for resource IDs and the component they effect
* Support for changing text color (unfortunately, this may be impossible)
* Support for previous (and future) versions of iTunes
* Support for x32 iTunes, if necessary