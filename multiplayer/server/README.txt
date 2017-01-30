To compile this project, you need Mono installed.

Mono is a cross platform open source framework which implements the .NET standard. (and supported by Microsoft)

Go here to download and install Mono: http://www.mono-project.com/

On Mac: you'll also want to make sure that Mono and MCS (its compiling command) are part of your console path by adding an EXPORT line to your bash profile. Look here: http://stackoverflow.com/questions/32542535/how-to-install-mono-on-macos-so-mono-works-in-terminal

Open your bash profile:

> vim ~/.bash_profile

And add this comment and line somewhere:

> # added for Mono for developing .Net applications/compiling .cs files into .exe files
> # "mcs" binary needed to compile, "mono" binary needed to run.
> export PATH="/Library/Frameworks/Mono.framework/Versions/Current/bin:$PATH"

After saving, you'll have to run your bash_profile again to get the updated path:

> source ~/.bash_profile

To test that your mono installation is working, try

> make test

It should compile hello_world.cs into hello_world.exe, and then run it, which will print:

> Hello world!

When you're ready to compile the server, type

> make

When you're ready to run the server, type:

> make run

If you have trouble with any of this, please feel free to contact me (benwritescode)


===================================================================

Bonus:

If you want to be able to open C# files in MonoDevelop from the command line, add MonoDevelop to your path, by adding this line to your bash_profile:

> # to open C# files with MonoDevelop from the command line. type 
> # monodevelop open file.cs
> export PATH="/Applications/Unity/MonoDevelop.app/Contents/MacOS:$PATH"

Then, to open a C# file from the command line, type:

> monodevelop myfile.cs

===================================================================

Bonus 2:

Make a shorter alias for "monodevelop" (md), and switch focus to MonoDevelop simultaneously. Add these lines to your ~/.bash_profile:

># alias for opening files with MonoDevelop from terminal. Also switches application focus to MonoDevelop manually.
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

Bonus 3:

If you want Sublime to have autocomplete for C# files, follow this tutorial. I haven't quite gotten OmniSharp to work with Sublime yet, though, so I'm currently using MonoDevelop on Mac. (see above)

http://makegamessa.com/discussion/2879/tutorial-using-sublime-text-3-in-unity-with-intellisense-autocomplete
Archives of the link: https://archive.fo/u7Wab, http://web.archive.org/web/20161006233039/http://makegamessa.com/discussion/2879/tutorial-using-sublime-text-3-in-unity-with-intellisense-autocomplete

