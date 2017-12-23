using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

//[System.Serializable]
//public class AudioInspectorTest
//{
//    public MonoBehaviour theScript;
//    public List<MethodInfo> itsFunctions;
//}

[ExecuteInEditMode]
public class InspectorTest : MonoBehaviour
{
    public AudioSource myAudioSource;

    //public List<AudioInspectorTest> audioEvents = new List<AudioInspectorTest>();

    public UnityAction someEvent;

    public static event Action<UnityAction> ToggleEvent;

    //public List<MethodInfo> methods = new List<MethodInfo>();
    //public MonoBehaviour monoBehav;
    //BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

    MethodInfo[] methodInfos;

    

	// Use this for initialization
	void Start ()
    {
        //audioEvents[0].thisFunction.changeEvent += PlaySound;

        //MethodBase methodBase = MethodBase.GetCurrentMethod();
        //Debug.Log(methodBase.Name);

        methodInfos = typeof(InspectorTest).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        
        foreach(MethodInfo methodInfo in methodInfos)
        {
            Debug.Log(methodInfo.Name);
        }

        //Get the method names that have been called - compare it to the one in UnityEvent?
    }

	
    // -- These are all to call the function needed -- //

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("e"))
        {
            PlaySound();
        }

        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(1);

        Debug.Log(stackTrace.GetFrame(0).GetMethod().Name);
        //Debug.Log("this works");
    }

    public void PlaySound()
    {
        //Debug.Log("A sound is played for a function");
    }

    //public void TestFunction(UnityEvent unityEvent)
    //{
    //    if (changeEvent != null) //Checking is anyone is "on the other line"
    //    {
    //        Debug.Log("Sending the sound event");
    //        changeEvent();
    //    }
    //}

    public void DoProcessing()
    {

    }

}
