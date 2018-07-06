using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptForTexture : MonoBehaviour {
    public GameObject ImageOnPanel;  ///set this in the inspector

    // Use this for initialization
    void Start () {
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(LoadImageAndDisplay.fileContents);
        GetComponent<Renderer>().material.mainTexture = tex;

        GetComponent<RawImage>().texture = tex;

    }

    public Texture returntext()
    {
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(LoadImageAndDisplay.fileContents);
        GetComponent<Renderer>().material.mainTexture = tex;

        GetComponent<RawImage>().texture = tex;
        return tex;
    }
	
	
}
