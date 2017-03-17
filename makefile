

# based on:
# https://pypi.python.org/pypi/SpeechRecognition/
# http://cmusphinx.sourceforge.net/wiki/tutorialpocketsphinx



speech_recognition:
	brew install portaudio
	pip install pyaudio
	pip install SpeechRecognition

pocketsphinx: sphinxbase
	git clone https://github.com/cmusphinx/pocketsphinx.git
	cd pocketsphinx

sphinxbase: swig
	git clone https://github.com/cmusphinx/sphinxbase.git
	cd sphinxbase

swig:
	brew install swig











