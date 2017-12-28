using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

[System.Serializable]
public class AudioInspectorTest
{
    public UnityAction thisFunction;
}

[ExecuteInEditMode]
public class InspectorTest : MonoBehaviour
{
    public AudioSource myAudioSource;

    public UnityAction someEvent;

    public static event Action<UnityAction> ToggleEvent;

    //public List<MethodInfo> methods = new List<MethodInfo>();
    //public MonoBehaviour monoBehav;
    //BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

    MethodInfo[] methodInfos;

    public delegate void ChangeEvent();
    public static event ChangeEvent changeEvent;

    // Use this for initialization
    void Start ()
    {
        changeEvent += PlaySound;


        methodInfos = typeof(InspectorTest).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        
        foreach(MethodInfo methodInfo in methodInfos)
        {
            //Debug.Log(methodInfo.Name);
        }

        //Get the method names that have been called - compare it to the one in UnityEvent?
    }

	
    // -- These are all to call the function needed -- //

	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown("e"))
        //{
        //    PlaySound();
        //}   
    }

    public void ReceivingSound()
    {
        if (changeEvent != null) //Checking is anyone is "on the other line"
        {
            //Sending the sound event
            Debug.Log("Sending the sound event");
            changeEvent();

            //Checking which function sent the sound event - OH MY GOD THIS ACTUALLY FUCKING WORKS
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            string functionCalled = stackTrace.GetFrame(1).GetMethod().Name;
            CompareFunctionNames(functionCalled);
        }
    }

    public void CompareFunctionNames(string functionName)
    {
        Debug.Log("Hey look at me I'm comparing function names with" + functionName);
    }

    public void PlaySound()
    {

    }

    public void DoProcessing()
    {

    }

}
