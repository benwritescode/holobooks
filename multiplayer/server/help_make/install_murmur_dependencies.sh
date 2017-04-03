#!/bin/sh

if [[ $EUID -eq 0 ]]; then 

	echo "======================================================"
	echo "Running as normal user.";
	echo "Please run make one time as root to install necessary dependencies."; 
	echo "======================================================"

	exit 1;

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


	# && git checkout --track -b 1.2.x origin/v1.2.x \

	

	touch .murmur_dependencies;
fi; 