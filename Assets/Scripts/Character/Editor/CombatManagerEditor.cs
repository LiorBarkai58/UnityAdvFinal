using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CombatManager))]
public class CombatManagerEditor : Editor
{
    private float customDamage = 20f; // Default custom damage

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        CombatManager combatManager = (CombatManager)target;


        GUILayout.Space(10);

        GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel);
        headerStyle.fontSize = 12;
        headerStyle.normal.textColor = Color.white;
        GUI.backgroundColor = Color.gray;

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Combat Debug Tools", headerStyle);
        EditorGUILayout.EndVertical();

        GUILayout.Space(10);

        if (GUILayout.Button("Take 10 Damage", GUILayout.Height(25)))
        {
            combatManager.TakeDamage(new DamageArgs{Damage = 10f});
        }

        GUILayout.Space(10);

        customDamage = EditorGUILayout.FloatField("Custom Damage", customDamage);

        if (GUILayout.Button("Take Custom Damage", GUILayout.Height(25)))
        {
            combatManager.TakeDamage(new DamageArgs{Damage = customDamage});
        }
    }
}