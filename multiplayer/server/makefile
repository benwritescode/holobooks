# type "make" to compile and run the server.

# makefile to compile and run server.
# compiling/running Mono: https://codetuts.tech/compile-c-sharp-command-line/

# To get ready to build on Mac: follow tutorial here: http://www.mono-project.com/docs/compiling-mono/mac/
# mac terminal: http://stackoverflow.com/questions/8480157/building-and-running-monodevelop-solution-in-os-x-terminal
# linux terminal: http://stackoverflow.com/questions/7437837/building-c-sharp-console-project-from-linux-terminal-using-mono



# If server.exe exists, execute it using the "mono" command.
run: server.exe
	mono server.exe

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

# for testing Mono installation.
test: helloworld.exe
	mono helloworld.exe

helloworld.exe: ./tests/helloworld/helloworld.cs
	mcs ./tests/helloworld/helloworld.cs
	cp ./tests/helloworld/helloworld.exe ./helloworld.exe

# target to open monodevelop project
# dev:
# 	md server.sln






