using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Ability), true)] // Apply to Ability and derived classes
public class AbilityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws default fields

        Ability ability = (Ability)target;

        EditorGUILayout.Space(10); // Adds spacing for better UI layout
        EditorGUILayout.LabelField("Ability Info", EditorStyles.boldLabel);

        // Display current level (readonly)
        EditorGUILayout.LabelField("Current Level:", ability.Level.ToString(), EditorStyles.helpBox);

        // Upgrade Button
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 12,
            fontStyle = FontStyle.Bold,
            fixedHeight = 30
        };

        if (GUILayout.Button("Upgrade Ability", buttonStyle))
        {
            ability.UpgradeAbility();
            EditorUtility.SetDirty(ability); // Marks object as changed
        }
    }
}