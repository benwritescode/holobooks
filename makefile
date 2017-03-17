

# based on:
# https://pypi.python.org/pypi/SpeechRecognition/
# http://cmusphinx.sourceforge.net/wiki/tutorialpocketsphinx
all: speech_recognition pocketsphinx


speech_recognition:
	brew install portaudio
	pip install pyaudio
	pip install SpeechRecognition

pocketsphinx: sphinxbase
	git clone https://github.com/cmusphinx/pocketsphinx.git || True
	cd ./pocketsphinx && ./autogen.sh
	cd ./pocketsphinx && ./configure
	cd ./pocketsphinx && make
	cd ./pocketsphinx && make install

sphinxbase: swig
	git clone https://github.com/cmusphinx/sphinxbase.git || True
	cd ./sphinxbase && ./autogen.sh
	cd ./sphinxbase && ./configure
	cd ./sphinxbase && make
	cd ./sphinxbase && make install

swig:
	brew install swig











