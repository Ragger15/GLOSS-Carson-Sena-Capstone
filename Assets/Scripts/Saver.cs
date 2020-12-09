using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Saver : MonoBehaviour
{
    [SerializeField] GridH gid;

    public void Save(TMP_InputField bob)
    {
        string path = Path.Combine(Application.persistentDataPath, Path.Combine("Maps", bob.text + ".json"));
        string jsonString = JsonUtility.ToJson(gid.GetSaveData());
        System.IO.File.WriteAllText(path, jsonString);
    }

    public void Load(TMP_InputField bob)
    {
        string jsonString = System.IO.File.ReadAllText(Path.Combine(Application.persistentDataPath, Path.Combine("Maps", bob.text + ".json")));
        gid.Load(JsonUtility.FromJson<GridSaveData>(jsonString));
    }
}
