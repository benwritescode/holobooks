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

One last step has to be manual: you need to add Pocket Sphinx to a library path in your .bash_profile. Add these lines to your .bash_profile:
  export LD_LIBRARY_PATH=/usr/local/lib:$LD_LIBRARY_PATH
  export PKG_CONFIG_PATH=/usr/local/lib/pkgconfig:$PKG_CONFIG_PATH


=======
# Google Cloud API installation instructions. 

Next, install Google Cloud API for our Google Cloud features:

https://cloud.google.com/sdk/docs/quickstart-mac-os-x

You can use Google's interactive installer by downloading and running their provided Bash script:

curl https://sdk.cloud.google.com | bash

It will request an installation directory.

=====