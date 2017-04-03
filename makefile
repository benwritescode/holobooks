# client make file
# based on:
# https://pypi.python.org/pypi/SpeechRecognition/
# http://cmusphinx.sourceforge.net/wiki/tutorialpocketsphinx
all: speech_recognition pocketsphinx

speech_recognition:
	brew install portaudio
	pip install pyaudio
	pip install SpeechRecognition
	# interface to pocketsphinx
	pip install pocketsphinx

# pocketsphinx library
pocketsphinx: sphinxbase
	git clone https://github.com/cmusphinx/pocketsphinx.git || True
	cd ./pocketsphinx && ./autogen.sh
	cd ./pocketsphinx && ./configure
	cd ./pocketsphinx && make
	cd ./pocketsphinx && make install


# pocketsphinx depends on sphinxbase
sphinxbase: swig
	git clone https://github.com/cmusphinx/sphinxbase.git || True
	cd ./sphinxbase && ./autogen.sh
	cd ./sphinxbase && ./configure
	cd ./sphinxbase && make
	cd ./sphinxbase && make install

#sphinxbase depends on swig
swig:
	brew install swig









