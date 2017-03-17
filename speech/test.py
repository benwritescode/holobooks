#!/usr/bin/env python

# based on example from https://github.com/Uberi/speech_recognition/blob/master/examples/audio_transcribe.py

import speech_recognition as sr

from os import path
AUDIO_FILE = path.join(path.dirname(path.realpath(__file__)), "abraham_lincoln.wav")


recog = sr.Recognizer()
with sr.AudioFile(AUDIO_FILE) as source:
	audio = recog.record(source)


# recognize speech using Sphinx
try:
    print("Output: " + recog.recognize_sphinx(audio))
except sr.UnknownValueError:
    print("Failed to understand audio")
except sr.RequestError as e:
	print("Error: {0}".format(e))


