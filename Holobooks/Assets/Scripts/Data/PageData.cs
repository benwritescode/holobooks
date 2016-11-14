using UnityEngine;
using System.Collections;

// enum examples
// https://www.dotnetperls.com/enum



public class PageData
{
	// no data guaranteed for a pagedata.

	public Texture2D image;
	public string text;

	public PageData ()
	{

	}

	public PageData (string nText)
	{
		this.text = nText;
	
	}

	public PageData (Texture2D nImage)
	{
		this.image = nImage;
	}

}
