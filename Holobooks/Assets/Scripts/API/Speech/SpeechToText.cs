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
	public static SpeechToText instance;

	// Here is the delegate type for returning the text result.
	public delegate void SpeechToTextCallback (string text);


	// currentlyProcessingSpeech bool allows us to avoid locking a mutex
	// *unless* we're in the middle of processing speech.
	private bool currentlyProcessingSpeech;

	// ============= MUTEX ONLY VARIABLES - only access these inside of lock(mutex) {}
	// mutex in order to manage *only* returning text on the main thread.
	private readonly object mutex = new object ();
	// If the output is ready, we call the mainCallback
	private bool outputReady = false;
	private SpeechToTextCallback mainCallback;
	// the text we got from speech recognition. Call mainCallback with this text.
	private string finalSpeechText = "";
	// ============= MUTEX ONLY VARIABLES END

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			return;  
	}
	// Use this for initialization
	void Start ()
	{



	}

	

	// reset all mutex-only variables.
	private void ResetMutexVariables (SpeechToTextCallback callback = null)
	{
		lock (mutex) {
			outputReady = false;
			mainCallback = callback;
			finalSpeechText = "";
		}
	}

	// this function locks the mutex, and returns the speech recognized text if it's ready.
	// The reason we only call the callback on the main thread is because some System/Unity functions can only be called on main thread
	// Before, I was calling the callback on my secondary thread. But this had the side effect of running the caller's callback not on main thread
	// That can cause errors.
	// this fix prevents those errors by making sure that anyone who uses this class is assured that their own code is always run
	// on the primary thread.
	private void ReturnTextOutputIfReady ()
	{
		
		// lock on the mutex to avoid race condition with speech recognition thread
		lock (mutex) {
			// if output is ready, return it to our client
			if (outputReady) {
				mainCallback (finalSpeechText);
				// don't return the text more than once, and stop.
				outputReady = false;
				currentlyProcessingSpeech = false;
			}
		}

	}

	public void ConvertClipToTextWithCallback (AudioClip clip, SpeechToTextCallback callback)
	{


		string dataPath = Application.dataPath + "/Scripts/API/Speech";

//		UnityEngine.Debug.Log ("SpeechToText: dataPath: " + dataPath);
		string pythonPath = Config.PythonPath ();

		// save the audio clip as a .wav file
		SavWav.Save (dataPath, "clip", clip);

		// Start a new thread to translate the speech into text
		// We have to call an outside process. Since the process can require indeterminate time, 
		// we don't want to wait for it on the main thread (which would cause a graphical freeze)
		// instead, we offload this work to a new thread.
		// when that new thread is completed, it will call the client's provided callback, along with the recognized speech.

		currentlyProcessingSpeech = true;

		// reset mutex variables.
		this.ResetMutexVariables (callback);

		Thread newThread = new Thread (new ThreadStart (() => this.SpeechToTextThread (clip, dataPath, pythonPath)));
		newThread.Start ();

	}
		
	// Update is called once per frame
	void Update ()
	{
		if (currentlyProcessingSpeech) {
			this.ReturnTextOutputIfReady ();
		}
	}


	void SpeechToTextThread (AudioClip clip, string dataPath, string pythonPath)
	{
		Process process = new Process ();

		process.StartInfo.CreateNoWindow = true;

		process.StartInfo.UseShellExecute = false;

		process.StartInfo.RedirectStandardOutput = true;

		process.StartInfo.FileName = pythonPath;

		process.StartInfo.Arguments = "convert_speech.py";

		process.StartInfo.WorkingDirectory = dataPath;

//		UnityEngine.Debug.Log ("pythonpath: " + pythonPath);
//		UnityEngine.Debug.Log ("dataPath: " + dataPath);

		int code = -2;
		string output = "";

		try {
			process.Start ();

			output = process.StandardOutput.ReadToEnd ();

			// wait until completion, and then call the callback
			process.WaitForExit ();
//			output = "o wow";

		} catch (Exception e) {

			UnityEngine.Debug.Log ("SpeechToTextThread: conversion failed: " + e.ToString ());
			UnityEngine.Debug.Log ("Python path: " + pythonPath);
			UnityEngine.Debug.Log ("Data path: " + dataPath);


		} finally {
			code = process.ExitCode;

			process.Dispose ();
			process = null;

			// when we're done, lock the mutex
			// indicate the output is ready for consuming

			lock (mutex) {
				outputReady = true;

				finalSpeechText = output;

			}


		}


	}

}
