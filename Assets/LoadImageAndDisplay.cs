using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Storage;
using System.Threading.Tasks;

public class LoadImageAndDisplay : MonoBehaviour {
    public static byte[] fileContents;
    string url = "https://firebasestorage.googleapis.com/v0/b/unity-ec9f0.appspot.com/o/Civita_Castellana.jpg?alt=media&token=cf27532d-8aa6-47fc-85bd-6ed9d4d0c7aa";
    Texture2D txt;

    IEnumerator LoadImg()
    {
        yield return 0;
        WWW imglink = new WWW(url);
        yield return imglink;
        txt = imglink.texture;
    }

    // Use this for initialization
    void Start() {
        StartCoroutine(LoadImg());
       /* Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Set a flag here indiciating that Firebase is ready to use by your
                // application.
                ;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

      

        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference storage_ref =
         storage.GetReferenceFromUrl("gs://unity-ec9f0.appspot.com");


        // Points to "images"
       // Firebase.Storage.StorageReference images_ref = storage_ref.Child("images");


        // Create a reference from a Google Cloud Storage URI
        Firebase.Storage.StorageReference gs_reference =
          storage.GetReferenceFromUrl("gs://unity-ec9f0.appspot.com/Civita_Castellana.jpg");

        // Download in memory with a maximum allowed size of 1MB (1 * 1024 * 1024 bytes)
          const long maxAllowedSize = 2900000;
          gs_reference.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task) => {
              if (task.IsFaulted || task.IsCanceled)
              {
                  Debug.Log(task.Exception.ToString());
                  // Uh-oh, an error occurred!
              }
              else
              {
                   fileContents = task.Result;
                  Debug.Log("Finished downloading!");
              }
          });
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(fileContents);
        this.gameObject.GetComponent<CanvasRenderer>().GetMaterial().mainTexture=tex;        /*
      // Create local filesystem URL
      string local_url = "file:///sdcard/Download/firebase.jpg";

      // Download to the local filesystem
      gs_reference.GetFileAsync(local_url).ContinueWith(task => {
          if (!task.IsFaulted && !task.IsCanceled)
          {
              Debug.Log("File downloaded.");
          }
      });*/
    }

    void OnGUI()
    {
        //Texture2D tex = new Texture2D(2, 2);
        //tex.LoadImage(fileContents);
        //this.gameObject.GetComponent<CanvasRenderer>().GetMaterial().mainTexture = tex;
        GUILayout.Label(txt);
    }


}
