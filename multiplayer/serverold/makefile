# makefile to compile and run server.

# compiling/running Mono: https://codetuts.tech/compile-c-sharp-command-line/

# type make to compile
# type make run to run the server.

run: server
	mono server.exe

server: server.cs
	mcs server.cs


# for testing Mono installation.
test: helloworld.exe
	mono helloworld.exe

helloworld.exe: helloworld.cs
	mcs helloworld.cs

# delete executables
clean:
	rm *.exe