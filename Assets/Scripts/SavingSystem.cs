using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;

public class SavingSystem : MonoBehaviour
{
    public IEnumerator LoadLastScene(string saveFile)
    {
        Dictionary<string, object> state = LoadFile(saveFile);
        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (state.ContainsKey("lastSceneBuildIndex"))
        {
            sceneBuildIndex = (int)state["lastSceneBuildIndex"];
        }
        yield return SceneManager.LoadSceneAsync(sceneBuildIndex);
        RestoreState(state);
    }

    public void Save(string saveFile)
    {
        Dictionary<string, object> state = LoadFile(saveFile);
        CaptureState(state);
        SaveFile(saveFile, state);
    }
    public void Load(string saveFile)
    {
        RestoreState(LoadFile(saveFile));
    }

    public void Delete(string saveFile)
    {
        File.Delete(GetPathFromSaveFile(saveFile));
    }

    private Dictionary<string, object> LoadFile(string saveFile)
    {
        string savePath = GetPathFromSaveFile(saveFile);
        if (!File.Exists(savePath)) return new Dictionary<string, object>();
        using (FileStream stream = File.Open(savePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    private void SaveFile(string saveFile, object state)
    {
        string savePath = GetPathFromSaveFile(saveFile);
        print("Saving to " + savePath);
        using (FileStream stream = File.Open(savePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    private void CaptureState(Dictionary<string, object> state)
    {
        foreach (SavingEntities entities in FindObjectsOfType<SavingEntities>())
        {
            state[entities.GetUniqueIdentifier()] = entities.CaptureState();
        }
        state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
    }

    private void RestoreState(Dictionary<string, object> state)
    {
        foreach (SavingEntities entities in FindObjectsOfType<SavingEntities>())
        {
            string id = entities.GetUniqueIdentifier();
            if (!state.ContainsKey(id)) continue;
            entities.RestoreState(state[id]);
        }
    }

    private string GetPathFromSaveFile(string savefile)
    {
        return Path.Combine(Application.persistentDataPath, savefile + ".sav");
    }
}

