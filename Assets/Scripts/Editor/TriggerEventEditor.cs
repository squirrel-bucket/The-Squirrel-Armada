using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TriggerEvent))]
public class TriggerEventEditor : Editor
{
    SerializedProperty eventToTrigger;
    SerializedProperty temporaryDarkness;
    SerializedProperty permanentDarkness;
    SerializedProperty flashEvent;
    SerializedProperty audioEvent;

    void OnEnable()
    {
        eventToTrigger = serializedObject.FindProperty("eventToTrigger");
        temporaryDarkness = serializedObject.FindProperty("temporaryDarkness");
        permanentDarkness = serializedObject.FindProperty("permanentDarkness");
        flashEvent = serializedObject.FindProperty("flashEvent");
        audioEvent = serializedObject.FindProperty("audioEvent");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(eventToTrigger,
            new GUIContent("Action Type"));

        switch (eventToTrigger.intValue)
        {
            case 0:
                EditorGUILayout.PropertyField(temporaryDarkness, true);
                break;

            case 1:
                EditorGUILayout.PropertyField(permanentDarkness, true);
                break;

            case 2:
                EditorGUILayout.PropertyField(flashEvent, true);
                break;

            case 3:
                EditorGUILayout.PropertyField(audioEvent, true);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}