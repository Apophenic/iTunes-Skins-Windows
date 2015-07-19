# iTunes Skin Tools for Windows
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
_Note: As of iTunes 12.2, "iTunes.dll" is now "iTunesResources.dll"_

```iTunes\iTunes.Resources\iTunes.dll``` is a custom assembly file created by Apple for use in iTunes. It's a resource-only
 .dll file, meaning it contains primarily resources such as images as opposed to actual code. We're interested in the
 RCData, which is a collection of raw _.png_ files that are used for iTunes' user interface. Each file has a unique
 unsigned integer ID. You can view the dll structure (and manipulate it) using [Resource Hacker](http://www.angusj.com/resourcehacker/#download)
 or with Visual Studio (dotPeek can't parse it).

### iTunes.dll RCData IDs
-------------------------
[Click here](http://htmlpreview.github.io/?https://github.com/Apophenic/iTunes-Skin-Tools/blob/master/Resource%20IDs/ResourceIDs.html) for a [comprehensive] guide of how resource IDs effects the UI.

Note: For older versions of iTunes, most resource IDs remain unchanged, however some don't exist.

### What iTunes Skin Tools Does
---------------------------
iTunes Skin Tools currently supports extracting all image resource files from iTunes.dll as well as injecting resources
back into the .dll file. Extracted files are named after their corresponding resource ID (i.e. _5000_ will be
extracted as _5000.png_). In a typical use case, you'll extract all resources, edit IDs of your choosing, then inject
the modified files back into iTunes.dll.

### How To Use It
-----------------
Pre-compiled binary: [.exe](https://github.com/Apophenic/iTunes-Skins-Windows/tree/master/iTunesSkinTools/exe)
Batch scripts are also available [here](https://github.com/Apophenic/iTunes-Skins-Windows/tree/master/scripts)

~~~ shell
iTunesSkinTools.exe -op=extract|inject -itunesdir="D:\Program Files\iTunes" -workingdir="D:\Directory"
-createbackup=true
~~~
* _op_: __Extract__ will extract all files from iTunes.dll into _workingdir_, while __Inject__ will inject all
files from _workingdir_ into iTunes.dll.
* _itunesdir_: iTunes.exe's parent directory
* _createbackup_: Optional, will create a backup of iTunes.dll if one doesn't already exist. Default behavior is ```true```.

You may also restore an iTunesResources.dll backup by doing the following:
~~~ shell
iTunesSkinTools.exe -op=restore -itunesdir="D:\Program Files\iTunes"
~~~

### Compatibility
-----------------
* Support for latest iTunes (12.2.1.16)
* iTunes Versions 11.1.5 and greater
* x32 and x64 versions of iTunes

### Project Status
------------------
Current Features:
* iTunes resource injection for custom skins and themes
* iTunes resource extraction
* Restore backups
* An exhaustive guide (w/ images) for resource IDs and the component they effect

__Mac support is coming soon__