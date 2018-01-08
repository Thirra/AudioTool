using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTest : MonoBehaviour
{
    //Sound things
    public InspectorTest inspectorTest;

    // Use this for initialization
    void Start ()
    {
        //Checking for the sound things
        if (inspectorTest == null)
            inspectorTest = GetComponentInChildren<InspectorTest>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("e"))
        {
            TestFunction();
        }
    }

    public void TestFunction()
    {
        //More sound things
        inspectorTest.ReceivingSound();
    }
}
