using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersist : MonoBehaviour
{
    [SerializeField] GridSaveData data;
    [SerializeField] GameObject prefab;
    public int saveW;
    public int saveh;

    public void Make2(TMP_InputField bob)
    {
        
        GridH grid = Instantiate(prefab).GetComponent<GridH>();
        if (int.TryParse(bob.text.Trim(), out int size))
        {
            grid.width = size;
            grid.height = size;
            //grid.ManualCreate();
            data = grid.GetSaveData();
            saveh = size;
            saveW = size;
        }
        else
        {
            Debug.Log("not a number " + bob.text.Trim());
        }
        PlayGame();
    }

    public void Make(TMP_Text path)
    {
        //System.IO.File.WriteAllText(Path.Combine(Application.persistentDataPath, Path.Combine("Maps", path.text + ".json")), "Hello world");
        string jsonString = System.IO.File.ReadAllText(Path.Combine(Application.persistentDataPath, Path.Combine("Maps",path.text + ".json")));
        data = JsonUtility.FromJson<GridSaveData>(jsonString);
        saveh = data.height;
        saveW = data.width;
        PlayGame();
    }

    public void PlayGame()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(sceneName: "Map Editor");
    }

}
