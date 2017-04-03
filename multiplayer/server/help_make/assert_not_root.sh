#!/bin/sh

if [[ $EUID -eq 0 ]]; then 
	# nothing
	echo "======================================================"
	echo "Running as normal user.";
	echo "======================================================"

else 
	echo "======================================================"
	echo "Running as root." 
	echo "Next, please type \"make\" as a normal (not sudo or root) user to finish building murmur."; 
	echo "======================================================"
	exit 1;

fi;
