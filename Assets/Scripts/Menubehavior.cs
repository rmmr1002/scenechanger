using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class Menubehavior : MonoBehaviour {
     
     public void scenechanger(string name)
    {
        XRSettings.enabled=true;
        SceneManager.LoadScene(name);
    }

}
