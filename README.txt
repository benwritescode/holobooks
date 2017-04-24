# holobooks
holobooks

# Set up for development

First, install Unity version 5.4.3f1 Personal. Go here to download Unity:

https://unity3d.com/

Now we need to install PocketSphinx and Python SpeechRecognition. Since the process involves a fair number of steps, I created a makefile to help you out with installation of PocketSpinx and Python SpeechRecognition. In this directory, type:

make

And it will (hopefully) install both for you.

If not, you can install SpeechRecognition, PocketSphinx, and SphinxBase by following directions at their respective homepages:

http://cmusphinx.sourceforge.net/wiki/tutorialpocketsphinx
https://pypi.python.org/pypi/SpeechRecognition/

One last step for Pocket Sphinx has to be manual: you need to add Pocket Sphinx to a library path in your .bash_profile. Add these lines to your .bash_profile:
  export LD_LIBRARY_PATH=/usr/local/lib:$LD_LIBRARY_PATH
  export PKG_CONFIG_PATH=/usr/local/lib/pkgconfig:$PKG_CONFIG_PATH

There is one more thing you might need to configure. In config.json, you need to paste the path to your local Python binary. To do this, go to terminal on Linux or Mac, or PowerShell on Windows, and type:

which python

This will give you the path you need to put into config.json.

# Install a voice chat client

On the server side, we are using "Murmur," and open source server for voice chat. The corresponding client side application is called "Mumble." You should be able to install it fairly easily by visiting Mumble's download page. I was able to install it on Mac with their wizard in a few minutes. Visit the link below to download and install Murmur for your operating system:

http://www.mumble.com/mumble-download.php

If you need to debug your Murmur server, you can use this command to make sure the port is open:

nmap -p 64738 45.56.115.75

Check for these results from nmap:

"filtered" == port blocked by firewall
"closed" == port opened by firewall, but no server application is bound to the port
"open" == port opened by firewall, and a server application is bound to the port

===================================================================

Bonus:

If you want to be able to connect to your server with SSH without typing your password, you can generate an SSH key and add it to your login on the server by following this tutorial:

https://www.digitalocean.com/community/tutorials/how-to-set-up-ssh-keys--2

===================================================================

Bonus 2:

If you want to be able to open C# files in MonoDevelop from the command line, add MonoDevelop to your path, by adding this line to your bash_profile:

> # to open C# files with MonoDevelop from the command line. type 
> # monodevelop open file.cs
> export PATH="/Applications/Unity/MonoDevelop.app/Contents/MacOS:$PATH"

Then, to open a C# file from the command line, type:

> monodevelop myfile.cs

===================================================================

Bonus 3:

Make a shorter alias for "monodevelop" (md), and switch focus to MonoDevelop simultaneously. Add these lines to your ~/.bash_profile:

> # alias for opening files with MonoDevelop from terminal. Also switches application focus to MonoDevelop manually.
>function md_function() {	
>	# The below commented out line works, but it ultimately opens the file for editing under the binary named 'mono' inside of MonoDevelop instead of the main application 'MonoDevelop.app'. 
>	# you can use the line below on Linux, but you'll probably have to switch application focus to 'mono' manually.
>	# monodevelop $1 &
	
>	# I'm using osascript instead on Mac, because I can open the file in MonoDevelop, and switch focus.
> # You also shouldn't need to have a "monodevelop" alias setup for this to work on Mac.
>	osascript -e 'tell application "MonoDevelop" to open "'$PWD/$1'"'
>	osascript -e 'activate application "MonoDevelop"'
>}
>alias md=md_function

=====================================================================

Bonus 4:

If you want Sublime to have autocomplete for C# files, follow this tutorial. I haven't quite gotten OmniSharp to work with Sublime yet, though, so I'm currently using MonoDevelop on Mac. (see above)

http://makegamessa.com/discussion/2879/tutorial-using-sublime-text-3-in-unity-with-intellisense-autocomplete
Archives of the link: https://archive.fo/u7Wab, http://web.archive.org/web/20161006233039/http://makegamessa.com/discussion/2879/tutorial-using-sublime-text-3-in-unity-with-intellisense-autocomplete

=====================================================================

If you have trouble with anything in this README, please feel free to contact me (@benwritescode)

=====================================================================

