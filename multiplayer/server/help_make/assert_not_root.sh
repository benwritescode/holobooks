#!/bin/sh

if [[ $EUID > 0 ]]; then 
	# nothing
	echo "Running as normal user.";
else 
	echo "Running as root. Next, please type \"make\" as a normal (not sudo or root) user to finish building murmur."; 
	exit 1;

fi;
