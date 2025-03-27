using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CombatManager), true)]
public class CombatManagerEditor : Editor
{
    private float customDamage = 20f; // Default custom damage

    private void OnEnable(){
        CombatManager combatManager = (CombatManager)target;
        combatManager.OnTakeDamage += OnDamageTaken;
    }
    private void OnDisable(){
        CombatManager combatManager = (CombatManager)target;
        combatManager.OnTakeDamage -= OnDamageTaken;
    }
    // This is the named method to repaint the inspector
    private void OnDamageTaken(DamageArgs args)
    {
        Repaint();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CombatManager combatManager = (CombatManager)target;

        GUILayout.Space(10);

        // Custom Header Style
        GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 12,
            normal = { textColor = Color.white }
        };
        GUI.backgroundColor = Color.gray;

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Combat Debug Tools", headerStyle);
        EditorGUILayout.EndVertical();

        GUILayout.Space(10);

        // Display Current Health and Max Health
        EditorGUILayout.LabelField("Current Health:", combatManager.CurrentHealth.ToString(), EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Max Health:", combatManager.CurrentMaxHealth.ToString(), EditorStyles.boldLabel);

        GUILayout.Space(10);

        // Button to Take 10 Damage
        if (GUILayout.Button("Take 10 Damage", GUILayout.Height(25)))
        {
            combatManager.TakeDamage(new DamageArgs { Damage = 10f });
        }

        GUILayout.Space(10);

        // Custom Damage Field
        customDamage = EditorGUILayout.FloatField("Custom Damage", customDamage);

        if (GUILayout.Button("Take Custom Damage", GUILayout.Height(25)))
        {
            combatManager.TakeDamage(new DamageArgs { Damage = customDamage });
        }

        // Refresh Inspector when health changes
        if (GUI.changed)
        {
            EditorUtility.SetDirty(combatManager);
        }
    }
}
