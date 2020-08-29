using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PluginTest : MonoBehaviour
{
    public Text myText;
    public string textToReverse;
    private AndroidJavaObject javaclass;
    void Start()
    {
        CallNativePlugin();
    }
    public void CallNativePlugin()
    {
        javaclass = new AndroidJavaObject("com.example.stringconverter.StringConverter");
        javaclass.Call("CheckPlugin");
        string reversedString = javaclass.Call<string>("revertString", textToReverse);
        myText.text = reversedString;
    }
}
