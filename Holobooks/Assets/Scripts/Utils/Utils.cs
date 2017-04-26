using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Configuration;


public static class Utils{

	public static Color FromRGBA255( byte r, byte g, byte b, byte a )
	{
		return FromRGBA255( (float)r, (float)g, (float)b, (float)a );
	}

	public static Color FromRGBA255( float r, float g, float b, float a )
	{
		return new Color( r / 255f, g / 255f, b / 255f, a / 255f );
	}

	public static void SwapList<T>( List<T> list,int index1, int index2 )
	{
		T temp = list[index1];
		list[index1] = list[index2];
		list[index2] = temp;
	}

	public static void Move<T>( List<T> list, int old_index, int new_index )
	{
		T item = list[old_index];
		list.RemoveAt(old_index);
		list.Insert(new_index,item);
	}

	public static void GoToLastIndex<T>( List<T> list, int index )
	{
		T item = list[index];
		list.RemoveAt(index);
		list.Add(item);
	}

	public static void SwapList<T>( T[] list,int index1, int index2 )
	{
		T temp = list[index1];
		list[index1] = list[index2];
		list[index2] = temp;
	}

	public static void applyMaterial(Page gameObj, String filename)
	{
//		UnityEngine.Debug.Log(filename);
		Material mat = Resources.Load(filename, typeof(Material)) as Material;
		Renderer rend = gameObj.GetComponent<Renderer>();
		if (rend != null){
			rend.material = mat;
		}

	}
	public static void runDownloadScript(string volumeId){


		Process process = new Process ();
		process.StartInfo.CreateNoWindow = true;
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.FileName = Config.PythonPath();
		process.StartInfo.Arguments = "downloadBook.py "+ volumeId;

		process.StartInfo.WorkingDirectory =  Application.dataPath + "/Resources/";


		int code = -2;
		string output = "";

		try {
			process.Start ();
			output = process.StandardOutput.ReadToEnd ();
			process.WaitForExit ();


		} catch (Exception e) {
			UnityEngine.Debug.Log ("Download Fail");
		} finally {
			code = process.ExitCode;
			process.Dispose ();
			process = null;

//			callback ("" + output);

		}
//
	}

	public static List<BookReference> runSearchScript(string keyword, string queryType){


		Process process = new Process ();
		//		process.StartInfo.FileName = "python";
		process.StartInfo.CreateNoWindow = true;
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.FileName = Config.PythonPath();
		process.StartInfo.Arguments = "searchBook.py " + keyword + " " + queryType;

		process.StartInfo.WorkingDirectory =  Application.dataPath + "/Resources/";

		List<BookReference> list;
		int code = -2;
		string output = "";

		try {
			process.Start ();
			output = process.StandardOutput.ReadToEnd ();
			process.WaitForExit ();


		} catch (Exception e) {
			UnityEngine.Debug.Log ("Download Fail");
		} finally {
			code = process.ExitCode;
			process.Dispose ();
			process = null;
			string jsonToParse = ParserSpace.BookReferenceParser.GetJsonNamed ("data.json");
			list = ParserSpace.BookReferenceParser.ParseResponseToReferences (jsonToParse, true);

		}
		return list;

	}

}
