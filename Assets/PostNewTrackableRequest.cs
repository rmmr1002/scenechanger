using JsonFx.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using UnityEngine;
using System.Web;
using Newtonsoft.Json;

public class PostNewTrackableRequest : MonoBehaviour {

    public string name;
    public float width;
    public string image;
    public string application_metadata;
}

public class CloudUpLoading : MonoBehaviour
{

    public Texture2D texture;

    private string access_key = "6498e1a027b2a6472e7162a437ee6be50f1575a8";
    private string secret_key = "1a5e990f4a5bfa0eec827fcf75253db7b85168d4";
    private string url = @"https://vws.vuforia.com";
    private string targetName = "MyTarget"; // must change when upload another Image Target, avoid same as exist Image on cloud

    private byte[] requestBytesArray;

    public void Start()
    {
        CallPostTarget();
    }

    public void CallPostTarget()
    {
        StartCoroutine(PostNewTarget());
    }

    IEnumerator PostNewTarget()
    {

        string requestPath = "/targets";
        string serviceURI = url + requestPath;
        string httpAction = "POST";
        string contentType = "application/json";
        string date = string.Format("{0:r}", DateTime.Now.ToUniversalTime());

        Debug.Log(date);

        // if your texture2d has RGb24 type, don't need to redraw new texture2d
        Texture2D tex = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
        WWW imglink = new WWW("https://firebasestorage.googleapis.com/v0/b/unity-ec9f0.appspot.com/o/Preacher_El_Greeco.jpg?alt=media&token=45546859-3f82-4c7e-a215-700b2228f35e");
        tex = imglink.texture;
        byte[] image = tex.EncodeToPNG();

        string metadataStr = "Vuforia metadata";//May use for key,name...in game
        byte[] metadata = System.Text.ASCIIEncoding.ASCII.GetBytes(metadataStr);
        PostNewTrackableRequest model = new PostNewTrackableRequest();
        model.name = targetName;
        model.width = 64.0f; // don't need same as width of texture
        model.image = System.Convert.ToBase64String(image);

        model.application_metadata = System.Convert.ToBase64String(metadata);
        string requestBody = JsonConvert.SerializeObject(model);

        WWWForm form = new WWWForm();

        var headers = form.headers;
        byte[] rawData = form.data;
        headers["Host"] = url;
        headers["Date"] = date;
        headers["Content-Type"] = contentType;

        HttpWebRequest httpWReq = (HttpWebRequest)HttpWebRequest.Create(serviceURI);

        MD5 md5 = MD5.Create();
        var contentMD5bytes = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(requestBody));
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < contentMD5bytes.Length; i++)
        {
            sb.Append(contentMD5bytes[i].ToString("x2"));
        }

        string contentMD5 = sb.ToString();

        string stringToSign = string.Format("{0}\n{1}\n{2}\n{3}\n{4}", httpAction, contentMD5, contentType, date, requestPath);

        HMACSHA1 sha1 = new HMACSHA1(System.Text.Encoding.ASCII.GetBytes(secret_key));
        byte[] sha1Bytes = System.Text.Encoding.ASCII.GetBytes(stringToSign);
        MemoryStream stream = new MemoryStream(sha1Bytes);
        byte[] sha1Hash = sha1.ComputeHash(stream);
        string signature = System.Convert.ToBase64String(sha1Hash);

        headers["Authorization"] = string.Format("VWS {0}:{1}", access_key, signature);

        Debug.Log("<color=green>Signaturere"+ "</ color > ");
   

        WWW request = new WWW(serviceURI, System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model)), headers);
        yield return request;

        if (request.error != null)
        {
            Debug.Log("requestr: " + request.error);
        }
        else
        {
            Debug.Log("requestess");
            Debug.Log("returned" + request.text);
        }

    }
}
