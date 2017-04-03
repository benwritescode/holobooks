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
run: multiplayer start

# If server.exe exists, execute it using the "mono" command.
multiplayer: server.exe 
	mono server.exe

# If .murmur_installed has been touched, then run our murmur server using murmurd
# https://wiki.mumble.info/wiki/Running_Murmur#Starting_Murmur
murmur: .murmur_installed
	/etc/init.d/mumble-server start
	#sudo -i murmurd -ini /etc/mumble-server.ini -supw your_password_here [srv]

start:
	echo =================================================
	echo Holobooks multiplayer makefile: make start: Starting Murmur. Please type in \"make stop\" when you\'re done. Or \"make restart\" to restart the Murmur server.
	echo =================================================
	/etc/init.d/mumble-server start

restart:
	/etc/init.d/mumble-server restart

# stop murmur server.
# http://forums.debian.net/viewtopic.php?f=16&t=39168
stop:
	/etc/init.d/mumble-server stop

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
	sudo apt-get install software-properties-common
	# sudo add-apt-repository ppa:slicer/ppa
	# fixes for missing keys...?
	# sudo apt-key adv --recv-keys --keyserver keys.gnupg.net E21C4F884E1220C3
	# sudo apt-key adv --recv-keys --keyserver keys.gnupg.net 4E1220C3
	sudo apt-get update
	sudo apt-get install mumble-server
	# one time configuration of server. this will prompt you to make a password.
	sudo dpkg-reconfigure mumble-server
	touch .murmur_installed


# build and install from source
# http://wiki.mumble.info/wiki/BuildingLinux#Debian_or_Ubuntu
.murmur_installed_from_source:

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
	qmake -recursive main.pro CONFIG+=no-client
	make



# Binary installation (alternative)
# Don't use this for now. Debian package seems to work well.
# If you want to use this, consider looking here for instructions on starting using the binary instead of the package:
# https://wiki.mumble.info/wiki/Running_Murmur#Starting_Murmur
.murmur_installed_binary:
	curl http://mumble.info/snapshot/mumble-1.3.0~2198~g282d672~snapshot.tar.gz > mumble.tar.gz
	tar -xvzf mumble.tar.gz
	mv mumble-1.3.0~2198~g282d672~snapshot mumble
	touch .murmur_installed



# target to open monodevelop project
# dev:
# 	md server.sln






