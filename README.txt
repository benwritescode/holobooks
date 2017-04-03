# holobooks
holobooks

# Set up for development

First, install Unity version 5.4.3f1 Personal. Go here to download Unity:

https://unity3d.com/

Now we need to install PocketSphinx and Python SpeechRecognition. Since the process involves a fair number of steps, I created a makefile to help you out with installation of PocketSpinx and Python SpeechRecognition. In this directory, type:

make

And it will (hopefully) install both for you.

If not, you can install SpeechRecognition, PocketSphinx, and SphinxBase by following directions at their respective homepages:

http://cmusphinx.sourceforge.net/wiki/tutorialpocketsphinx
https://pypi.python.org/pypi/SpeechRecognition/

One last step for Pocket Sphinx has to be manual: you need to add Pocket Sphinx to a library path in your .bash_profile. Add these lines to your .bash_profile:
  export LD_LIBRARY_PATH=/usr/local/lib:$LD_LIBRARY_PATH
  export PKG_CONFIG_PATH=/usr/local/lib/pkgconfig:$PKG_CONFIG_PATH

# Install a voice chat client

On the server side, we are using "Murmur," and open source server for voice chat. The corresponding client side application is called "Mumble." You should be able to install it fairly easily by visiting Mumble's download page. I was able to install it on Mac with their wizard in a few minutes. Visit the link below to download and install Murmur for your operating system:

http://www.mumble.com/mumble-download.php

