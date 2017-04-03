# type "make" to compile the multiplayer server and install murmur.
# type "make run" to run multiplayer server and start the murmur server.

# makefile to compile and run server.
# compiling/running Mono: https://codetuts.tech/compile-c-sharp-command-line/

# To get ready to build on Mac: follow tutorial here: http://www.mono-project.com/docs/compiling-mono/mac/
# mac terminal: http://stackoverflow.com/questions/8480157/building-and-running-monodevelop-solution-in-os-x-terminal
# linux terminal: http://stackoverflow.com/questions/7437837/building-c-sharp-console-project-from-linux-terminal-using-mono

# installing/running Murmur: https://wiki.mumble.info/wiki/Murmurguide#Linux

# default: just build the server. Also, install Murmur.
default: server.exe .murmur_installed_from_source

# start multiplayer server and start the murmur server
run: start_murmur multiplayer 

# If server.exe exists, execute it using the "mono" command.
multiplayer: server.exe 
	mono server.exe

# If .murmur_installed_from_source has been touched, then run our murmur server using murmurd
# https://wiki.mumble.info/wiki/Running_Murmur#Starting_Murmur

start_murmur: start

start: .murmur_installed_from_source
	echo =================================================
	echo Holobooks multiplayer makefile: make start: Starting Murmur. Please type in \"make stop\" when you\'re done. Or \"make restart\" to restart the Murmur server.
	echo =================================================
	cd ./mumble/mumble-master/release && ./murmurd

# This longer start command didn't really work.
# # sudo -i murmurd -ini /etc/mumble-server.ini -supw your_password_here [srv]

# Whenever I tried with -supw or -ini, the port didn't open. I'm not sure exactly why.
# I found that just running ./murmurd was the only thing that would actually open the port.
# I tested whether the port was open using nmap on another computer:
# nmap -p 64738 (server ip address here)


# ask murmurd to stop by using pkill
# http://forums.debian.net/viewtopic.php?f=16&t=39168
stop:
	pkill murmurd

# If the Holobooks server.exe exists, copy the generated server.exe to the local directory
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


# build and install from source
# http://wiki.mumble.info/wiki/BuildingLinux#Debian_or_Ubuntu

# http://stackoverflow.com/questions/638975/how-do-i-tell-if-a-regular-file-does-not-exist-in-bash
# http://stackoverflow.com/questions/18215973/how-to-check-if-running-as-root-in-a-bash-script
.murmur_installed_from_source:

	if [[ $EUID > 0 ]]; then
		echo "Please run make one time as root."
	else


		# dependencies
		sudo apt-get install build-essential pkg-config qt5-default qtbase5-dev qttools5-dev qttools5-dev-tools libqt5svg5* \
		libspeex1 libspeex-dev libboost-dev libasound2-dev libssl-dev g++ \
		libspeechd-dev libzeroc-ice-dev ice-slice libpulse-dev slice2cpp \
		libcap-dev libspeexdsp-dev libprotobuf-dev protobuf-compiler \
		libogg-dev libavahi-compat-libdnssd-dev libsndfile1-dev \
		libg15daemon-client-dev libxi-dev 
		sudo apt-get install git
		sudo apt-get install zip	
		wget https://github.com/mumble-voip/mumble/archive/master.zip
		unzip master.zip -d mumble

		# checkout stable 1.2.x ...
		cd mumble/mumble-master \
		&& git submodule init \
		&& git submodule update \
		&& git pull

		# && git checkout --track -b 1.2.x origin/v1.2.x \
	
		# build it with qmake and make. Don't build the client. (this is on the server only anyway.)
		cd mumble/mumble-master \
		&& qmake -recursive main.pro CONFIG+=no-client \
		&& make

		touch .murmur_installed_from_source
	fi

# target to open monodevelop project
# dev:
# 	md server.sln






