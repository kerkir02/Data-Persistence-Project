using UnityEngine;
using TMPro;
using System.IO;

public class Saves : MonoBehaviour
{
    public static Saves Instance;

    public int highScore;
    public string playerName;

    public TMP_InputField inputField;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string playerName;
    }

    public void SaveColor()
    {
        SaveData saveData = new SaveData();
        saveData.highScore = highScore;
        saveData.playerName = playerName;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            highScore = saveData.highScore;
            playerName = saveData.playerName;
        }
    }

    public void SaveInput()
    {
        playerName = inputField.text;
    }
}
