using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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


}
