# type "make" to compile the multiplayer server and install murmur.
# type "make run" to run multiplayer server and start the murmur server.

# makefile to compile and run server.
# compiling/running Mono: https://codetuts.tech/compile-c-sharp-command-line/

# To get ready to build on Mac: follow tutorial here: http://www.mono-project.com/docs/compiling-mono/mac/
# mac terminal: http://stackoverflow.com/questions/8480157/building-and-running-monodevelop-solution-in-os-x-terminal
# linux terminal: http://stackoverflow.com/questions/7437837/building-c-sharp-console-project-from-linux-terminal-using-mono

# installing/running Murmur: https://wiki.mumble.info/wiki/Murmurguide#Linux

# default: just build the server. Also, install Murmur.
default: server.exe .murmur_installed

# start multiplayer server, start murmur server
run: multiplayer murmur

# If server.exe exists, execute it using the "mono" command.
multiplayer: server.exe 
	mono server.exe

# If .murmur_installed has been touched, then run our murmur server using murmurd
murmur: .murmur_installed
	murmurd

# If it exists, copy the generated server.exe to the local directory
server.exe: ./server/obj/x86/Debug/server.exe
	cp ./server/obj/x86/Debug/server.exe ./server.exe

# build server.exe by running "xbuild" on the C# / .Net solution file in the local directory
./server/obj/x86/Debug/server.exe: ./server/Program.cs
	xbuild server.sln

# delete executables
clean:
	rm ./server/obj/x86/Debug/server.exe
	rm ./server.exe

# for testing stuff.
test: test1 test2

test1: helloworld.exe
	mono helloworld.exe
	rm helloworld.exe

helloworld.exe: ./tests/helloworld/helloworld.cs
	mcs ./tests/helloworld/helloworld.cs
	cp ./tests/helloworld/helloworld.exe ./helloworld.exe

test2: jsontest.exe
	mono jsontest.exe
	rm jsontest.exe

jsontest.exe: tests/jsontest/jsontest/Program.cs
	xbuild ./tests/jsontest/jsontest.sln 
	cp tests/jsontest/jsontest/bin/Debug/jsontest.exe ./jsontest.exe

# hidden file .murmur_installed indicates whether we have already installed murmur.
.murmur_installed:
	sudo add-apt-repository ppa:slicer/ppa
	sudo apt-get update
	sudo apt-get install mumble-server
	sudo dpkg-reconfigure mumble-server
	touch .murmur_installed

# target to open monodevelop project
# dev:
# 	md server.sln






