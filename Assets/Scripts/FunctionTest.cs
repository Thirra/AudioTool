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
        Debug.Log("Hey I'm doing something");
        inspectorTest.ReceivingSound();
    }
}
