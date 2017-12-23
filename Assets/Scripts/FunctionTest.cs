using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTest : MonoBehaviour
{
    //Sound things?
    public delegate void ChangeEvent();
    public static event ChangeEvent changeEvent;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown("e"))
        //{
        //    TestFunction();
        //}
    }

    public void TestFunction()
    {
        Debug.Log("Hey I'm doing something");

        if (changeEvent != null) //Checking is anyone is "on the other line"
        {
            Debug.Log("Sending the sound event");
            changeEvent();
        }
    }
}
