using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;


[ExecuteAlways]
public class SavingEntities : MonoBehaviour
{
    [SerializeField] string uniqueIdentifier = "";
    static Dictionary<string, SavingEntities> globalLookup = new Dictionary<string, SavingEntities>();
    public string GetUniqueIdentifier()
    {
        return uniqueIdentifier;
    }

    public object CaptureState()
    {
        Dictionary<string, object> state = new Dictionary<string, object>();
        foreach (ISaveable saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = saveable.CaptureState();
        }
        return state;
    }

    public void RestoreState(object state)
    {
        Dictionary<string, object> stateDict = (Dictionary<string, object>)state;

        foreach (ISaveable saveable in GetComponents<ISaveable>())
        {
            string id = saveable.GetType().ToString();
            if (stateDict.ContainsKey(id)) saveable.RestoreState(stateDict[id]);
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Application.isPlaying) return;
        if (string.IsNullOrEmpty(gameObject.scene.path)) return;

        SerializedObject @object = new SerializedObject(this);
        SerializedProperty property = @object.FindProperty("uniqueIdentifier");

        if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
        {
            property.stringValue = Guid.NewGuid().ToString();
            @object.ApplyModifiedProperties();
        }

        globalLookup[property.stringValue] = this;
    }
#endif

    private bool IsUnique(string candidate)
    {
        if (!globalLookup.ContainsKey(candidate)) return true;
        if (globalLookup[candidate] == this) return true;
        if (globalLookup[candidate] == null)
        {
            globalLookup.Remove(candidate);
            return true;
        }
        if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
        {
            globalLookup.Remove(candidate);
            return true;
        }
        return false;
    }
}

