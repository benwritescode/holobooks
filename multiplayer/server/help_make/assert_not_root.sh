#!/bin/sh

if [[ $EUID > 0 ]]; then 
	
else \
	echo "Next, type \"make\" as a normal (not sudo or root) user to finish building murmur."; 
	return 1;

fi;
