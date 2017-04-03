To compile the Holobook server, you need Mono installed.

Mono is a cross platform open source framework which implements the .NET standard. (and supported by Microsoft)

Go here to download and install Mono for your OS: http://www.mono-project.com/

On Mac: you'll also want to make sure that Mono and MCS (its compiling command) are part of your console path by adding an EXPORT line to your bash profile. Look here: http://stackoverflow.com/questions/32542535/how-to-install-mono-on-macos-so-mono-works-in-terminal

Open your bash profile:

> vim ~/.bash_profile

And add this comment and line somewhere:

> # added for Mono for developing .Net applications/compiling .cs files into .exe files
> # "mcs" binary needed to compile, "mono" binary needed to run.
> export PATH="/Library/Frameworks/Mono.framework/Versions/Current/bin:$PATH"

After saving, you'll have to run your bash_profile again to get the updated path:

> source ~/.bash_profile

To test that your mono installation is working in the same directory as this README, try:

> make test

It should compile hello_world.cs into hello_world.exe, and then run it, which will print:

> Hello world!

When you're ready to compile and run the server, type

> make run

If you have trouble with any of this, please feel free to contact me (benwritescode)

===================================================================

The following instructions will build both Murmur (our chosen voice chat server), as well as the Holobooks server. They each run on different ports. The makefile has targets to build each of them. The advantage of this is that you can connect to the Murmur voice chat server using a Mumble client, and talk to other users of the Holobooks server.

Suggested Linux server installation and configuration:

Create a user called "holobooks". Then, whenever you want to run the Holobooks server application, you can switch user to the holobooks user. Once you are in the Holobooks user, you can use Screen to background the server application process. That way, when you log off, the server application will keep running. When you want to turn the server off, you can reconnect to the screen to exit the process. After SSHing into your server, follow the steps below:

# SSH into your server
ssh user@server

# Create user named "holobooks" and make a home directory for the user
# Make a new user. The password doesn't matter, as long as you have an admin account to use to switch to the user.
sudo useradd holobooks

# passwd - This step is optional to create a password for the user. You don't need to use the password if you're an administrator.
sudo passwd holobooks

# Make a home directory for the holobooks user
sudo mkhomedir_helper holobooks

# By default, new users in some Linux distributions don't have Bash as their default terminal, which means TAB key autocomplete won't work when you are using SSH. Fix it by switching the Holobooks' user's default login shell from /bin/sh to /bin/bash:
sudo chsh -s /bin/bash holobooks

# Before you switch to holobooks, we also need to install an application called "screen". This will allow us to run our server executable in the background, and disconnect from the server, and later reconnect to manage the server executable.
sudo apt-get install screen

# Switch to the user
sudo su holobooks

# Clone the project from github:
git clone https://github.com/benwritescode/holobooks.git

# Change directory into Holobooks project:
cd holobooks

# Change branches to benwritescode (in the future, all server code will be in the main branch, and it won't be necessary to switch branches)
git checkout benwritescode

# CD into server directory:
cd ./multiplayer/server

# Switch back to a root user. We need root privilege to install Murmur dependencies.
exit

# You need to type "sudo make" to get Murmur's dependencies. The makefile detects whether you are building as root.
sudo make

# Now switch back to the holobooks user:
sudo su holobooks

# Now type "make" once more to build both Murmur and the Holobooks server.
make

# Okay, that's the end of the one time setup.
# The rest of the steps should be performed each time you want to start the server.

# Now we're ready to run the server executable with "make run". However, you should put it in a screen, so it will keep running after you disconnect from the server. First, we must run "script /dev/null" so we can "own" this shell, which allows us to use the "screen" command:
script /dev/null

# Next, create a screen:
screen

# Now that you are in a screen, start the server executables. This starts both the Holobooks server and the Murmur server. Note that the Murmur server is backgrounded into another process by default, and the Holobooks server runs on the current process.
make run

# to disconnect from your screen, press CTRL-A, lift your fingers, and then press the D key

# to reconnect to your screen:
screen -r

# to exit (end) a screen, type:
exit

# When you're done using the Holobooks server, you can use CTRL-C to end the process.
# But Murmur is still running in the background. Use the following commands to manage the Murmur server independently of the Holobooks server process:

# to start Murmur server
make start

# to stop Murmur server
make stop

# You can look in the file multiplayer/server/makefile for more links about Murmur, and to see how I automated the process of building Murmur from source.

References:
https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/4/html/Step_by_Step_Guide/s1-starting-create-account.html
http://askubuntu.com/questions/335961/create-default-home-directory-for-existing-user-in-terminal
http://askubuntu.com/questions/325807/arrow-keys-tab-complete-not-working
https://help.github.com/articles/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent/
https://git-scm.com/book/en/v2/Git-Branching-Branches-in-a-Nutshell
https://makandracards.com/makandra/2533-solve-screen-error-cannot-open-your-terminal-dev-pts-0-please-check
http://www-users.cs.umn.edu/~gini/1901-07s/files/script.html
http://serverfault.com/questions/309052/check-if-port-is-open-or-closed-on-a-linux-server
https://www.digitalocean.com/community/tutorials/how-to-set-up-ssh-keys--2
http://stackoverflow.com/questions/24049992/json-net-on-ubuntu-linux
http://stackoverflow.com/questions/38118548/how-to-install-nuget-from-command-line-on-linux
https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference

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

