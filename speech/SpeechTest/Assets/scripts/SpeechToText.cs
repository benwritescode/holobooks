using UnityEngine;
using System.Collections;


// For threads
using System.Threading;

// For processes
using System.Diagnostics;
using System;


// get OS
// https://docs.unity3d.com/ScriptReference/SystemInfo-operatingSystem.html

// for getting path of Python
using Configuration;

public class SpeechToText : MonoBehaviour
{

	// Here is the delegate type for returning the text result.
	public delegate void SpeechToTextCallback (string text);

	// Use this for initialization
	void Start ()
	{



	}

	public void ConvertClipToTextWithCallback (AudioClip clip, SpeechToTextCallback callback)
	{

		string dataPath = Application.dataPath;
		string pythonPath = Config.PythonPath ();

		// save the audio clip as a .wav file
		SavWav.Save (dataPath, "clip", clip);


		// Start a new thread to translate the speech into text
		// We have to call an outside process. Since the process can require indeterminate time, 
		// we don't want to wait for it on the main thread (which would cause a graphical freeze)
		// instead, we offload this work to a new thread.
		// when that new thread is completed, it will call the client's provided callback, along with the recognized speech.
		Thread newThread = new Thread (new ThreadStart (() => this.SpeechToTextThread (clip, callback, dataPath, pythonPath)));
		newThread.Start ();

	}
		
	// Update is called once per frame
	void Update ()
	{
	
	}


	void SpeechToTextThread (AudioClip clip, SpeechToTextCallback callback, string dataPath, string pythonPath)
	{
		Process process = new Process ();

		process.StartInfo.CreateNoWindow = true;

		process.StartInfo.UseShellExecute = false;

		process.StartInfo.RedirectStandardOutput = true;

		process.StartInfo.FileName = pythonPath;

		process.StartInfo.Arguments = "convert_speech.py";

		process.StartInfo.WorkingDirectory = dataPath;


		int code = -2;
		string output = "";

		try {
			process.Start ();

			output = process.StandardOutput.ReadToEnd ();

			// wait until completion, and then call the callback
			process.WaitForExit ();


		} catch (Exception e) {

			UnityEngine.Debug.Log ("SpeechToTextThread: conversion failed: " + e.ToString ());


		} finally {
			code = process.ExitCode;
			process.Dispose ();
			process = null;

			callback ("" + output);

		}


	}

}
