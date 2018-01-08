using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CustomEditor(typeof(InspectorTest))]
[ExecuteInEditMode]
public class InspectorTestEditor : Editor
{
    InspectorTest inspectorTest;
    static bool showContent = true;

    public void OnEnable()
    {
        //Setting up the serialized properties
        inspectorTest = (InspectorTest)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty audioFunctions = serializedObject.FindProperty("functions");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("myAudioSource"), true);
        GUILayout.Space(10);

        if (inspectorTest.functions.Count > 0)
        {
            foreach(AudioInspectorTest audio in inspectorTest.functions)
            {
                GUILayout.Space(10);

                //Name of the audio function - not really needed but helps with organisation
                GUILayout.BeginHorizontal();
                GUILayout.Label("Name: ", GUILayout.Width(50));
                audio.name = GUILayout.TextField(audio.name, GUILayout.Width(200));
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                //Adding the UnityEvent to the inspector
                int currentIndex = inspectorTest.functions.IndexOf(audio);
                EditorGUILayout.PropertyField(audioFunctions.GetArrayElementAtIndex(currentIndex).FindPropertyRelative("thisFunction"), true);

                GUILayout.Space(5);

                //Adding the list of audio clips to the inspector
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);
                showContent = EditorGUILayout.Foldout(showContent, "Audio Required for Function");
                GUILayout.Space(150);

                //Add another audio clip
                if (GUILayout.Button("+"))
                {
                    AudioClip newAudioClip = new AudioClip();
                    audio.thisAudio.Add(newAudioClip);
                    serializedObject.Update();
                }

                //Remove an audio clip
                if (GUILayout.Button("-"))
                {
                    if (audio.thisAudio.Count > 1)
                    {
                        int removeIndex = audio.thisAudio.Count - 1;
                        audioFunctions.DeleteArrayElementAtIndex(currentIndex);
                        audio.thisAudio.RemoveAt(removeIndex);
                        //Do I need this?
                        serializedObject.Update();
                    }
                }
                GUILayout.EndHorizontal();

                //Customising the look of the list of audio clips - each one can be named for organisational purposes
                if (audio.thisAudio.Count > 0)
                {
                    for (int index2 = 0; index2 < audio.thisAudio.Count; index2++)
                    {
                        SerializedProperty thisAudioProp = audioFunctions.GetArrayElementAtIndex(currentIndex).FindPropertyRelative("thisAudio");
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(20);
                        string name = GUILayout.TextField("Audio " + (index2 + 1), GUILayout.Width(80));
                        EditorGUILayout.PropertyField(thisAudioProp.GetArrayElementAtIndex(index2), GUIContent.none);
                        GUILayout.EndHorizontal();
                    }
                }

                //Adding the audio enum to the inspector
                EditorGUILayout.PropertyField(audioFunctions.GetArrayElementAtIndex(currentIndex).FindPropertyRelative("audioType"), true);

                //Adding the delay or repeat inspector properties according to which enum is selected
                int audioTypeIndex = (int)audio.audioType;
                audioTypeActivation(audioTypeIndex, currentIndex);

                //Removing a specific audio function
                if (GUILayout.Button("Remove This Audio"))
                {
                    RemoveFunction(currentIndex);
                    serializedObject.Update();
                }
            }
        }
        GUILayout.Space(10);

        //Adding another audio function
        if (GUILayout.Button("Add Audio Function"))
        {
            AddFunction();
        }

        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Adding a new audio function
    /// </summary>
    void AddFunction()
    {
        AudioInspectorTest addAudio = new AudioInspectorTest();
        inspectorTest.functions.Add(addAudio);
        serializedObject.Update();
    }

    /// <summary>
    /// Removing an audio function by a specific index
    /// </summary>
    /// <param name="thisIndex"></param>
    void RemoveFunction(int thisIndex)
    {
        if (inspectorTest.functions.Count > 1)
        {
            //int removeIndex = inspectorTest.functions.Count - 1;
            inspectorTest.functions.RemoveAt(thisIndex);
        }
    }

    /// <summary>
    /// Changes the inspector according to which audio enum is selected, so that uncessary values aren't shown
    /// </summary>
    /// <param name="enumNumber"></param>
    /// <param name="indexNumber"></param>
    public void audioTypeActivation(int enumNumber, int indexNumber)
    {
        switch(enumNumber)
        {
            //Play one shot
            case 0:
                {

                }
                break;

            //Play random
            case 1:
                {

                }
                break;

            //Play after delay
            case 2:
                {
                    //Showing the "delay amount" inspector property
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(200);
                    GUILayout.Label("Delay Amount: ");
                    EditorGUILayout.FloatField(inspectorTest.functions[indexNumber].delayAmount, GUILayout.Width(80));
                    GUILayout.Space(130);
                    GUILayout.EndHorizontal();
                }
                break;

            //Play pause after time
            case 3:
                {
                    //Showing the "delay amount" inspector property
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(200);
                    GUILayout.Label("Delay Amount: ");
                    EditorGUILayout.FloatField(inspectorTest.functions[indexNumber].delayAmount, GUILayout.Width(80));
                    GUILayout.Space(130);
                    GUILayout.EndHorizontal();

                    //Showing the "repeat amount" inspector property
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(200);
                    GUILayout.Label("Repeat Amount: ");
                    EditorGUILayout.IntField(inspectorTest.functions[indexNumber].repeatAmount, GUILayout.Width(80));
                    GUILayout.Space(130);
                    GUILayout.EndHorizontal();
                }
                break;
        }
    }
}
