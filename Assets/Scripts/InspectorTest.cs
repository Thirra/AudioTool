using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

/// <summary>
/// How the audio for a specific function should be played
/// When added to - add a specific switch in the custom editor
/// </summary>
public enum AudioFunctions
{
    PlayOneShot,
    PlayRandom,
    PlayAfterDelay,
    PlayPauseOverTime
};

/// <summary>
/// The class that holds all the information for the audio that will play for a specific function
/// </summary>
[System.Serializable]
public class AudioInspectorTest
{
    public string name;
    public UnityEvent thisFunction;
    public List<AudioClip> thisAudio = new List<AudioClip>();
    public AudioFunctions audioType;
    public float delayAmount;
    public int repeatAmount;
}

[ExecuteInEditMode]
public class InspectorTest : MonoBehaviour
{
    public AudioSource myAudioSource;

    //The list of the classes for every function needing audio on the gameobject
    public List<AudioInspectorTest> functions;
    
    //Getting access to the method data
    MethodInfo[] methodInfos;

    //Creating an event for functions to subscribe to, to see when a specific function has been called
    public delegate void FunctionCalled();
    public static event FunctionCalled functionCalled;

    private void Start ()
    {
        //Subscribing to the event 
        functionCalled += CheckFunction;

        //Don't actually need this but it was pretty cool to learn about
        methodInfos = typeof(InspectorTest).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
    }

    public void ReceivingSound()
    {
        functionCalled();
    }

    /// <summary>
    /// Checking the function that is called, and the functions within the list of audio functions, and comparing them by name to check if there is a match.
    /// If there is a match, continue to the play sound function.
    /// </summary>
    public void CheckFunction()
    {
        //Checking which function sent the sound event - OH MY GOD THIS ACTUALLY FUCKING WORKS
        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();

        //Change the "GetFrame" to however deep you need to go to find the function that was called: TestFunction -> Receiving Sound -> this
        string functionCalled = stackTrace.GetFrame(2).GetMethod().Name;
        for (int index = 0; index < functions.Count; index++)
        {
            //If the function called and the function in the list of audio functions are the same...
            if (functionCalled == functions[index].thisFunction.GetPersistentMethodName(0))
            {
                //Play the appropriate sound
                PlaySound(index);
            }
            else
            {
                //Do nothing
            }
        }
    }

    /// <summary>
    /// Playing the audio associated with the function that has been called.
    /// </summary>
    /// <param name="functionIndex"></param>
    public void PlaySound(int functionIndex)
    {
        //The enumerator of all the possible sound types currently available. All are pretty self explanatory
        switch (functions[functionIndex].audioType)
        {
            case AudioFunctions.PlayOneShot:
                {
                    myAudioSource.clip = functions[functionIndex].thisAudio[0];
                    myAudioSource.Play();
                }
                break;
                
            case AudioFunctions.PlayRandom:
                {
                    int randomIndex = Random.Range(0, functions[functionIndex].thisAudio.Count);
                    myAudioSource.clip = functions[functionIndex].thisAudio[randomIndex];
                    myAudioSource.Play();
                }
                break;

            case AudioFunctions.PlayAfterDelay:
                {
                    myAudioSource.clip = functions[functionIndex].thisAudio[0];
                    StartCoroutine(audioDelay(functions[functionIndex].delayAmount));
                }
                break;

            case AudioFunctions.PlayPauseOverTime:
                {
                    myAudioSource.clip = functions[functionIndex].thisAudio[0];
                    for (int index = 0; index < functions[functionIndex].repeatAmount; index++)
                    {
                        StartCoroutine(audioDelay(functions[functionIndex].delayAmount));
                    }
                    Debug.Log("Finished");
                }
                break;
        }
    }

    public IEnumerator audioDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        myAudioSource.Play();
    }
}
