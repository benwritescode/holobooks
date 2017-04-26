#!/usr/bin/env python

# based on example from https://github.com/Uberi/speech_recognition/blob/master/examples/audio_transcribe.py

import os
cwd = os.getcwd()
audioFile = cwd+"/clip.wav"


import speech_recognition as sr

# from os import path
# mypath = path.dirname(path.realpath(__file__))
# my_audio_file = path.join(cwd, "clip.wav")
# print("my audio file is: " + my_audio_file);

recog = sr.Recognizer()
with sr.AudioFile(audioFile) as source:
	audio = recog.record(source)

# recognize speech using Sphinx
try:
	my_speech = recog.recognize_sphinx(audio)
	print("" + my_speech)
	# f = open(cwd+"/speech.txt", 'w')
	# f.write(my_speech)
	# f.close()
except sr.UnknownValueError:
	print("Failed to understand audio")
except sr.RequestError as e:
	print("Error: {0}".format(e))


