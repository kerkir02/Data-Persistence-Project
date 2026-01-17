using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class Saves : MonoBehaviour
{
    public static Saves Instance;

    public int highScore;
    public string hsPlayerName;
    public string playerName;

    public TMP_InputField inputField;
    public TMP_Text bestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();

        inputField.onValueChanged.AddListener(OnTextChanged);
        bestScore.text = BestScoreChange();
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string hsPlayerName;
    }

    public void SaveScore()
    {
        SaveData saveData = new SaveData();
        saveData.highScore = highScore;
        saveData.hsPlayerName = hsPlayerName;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            hsPlayerName = saveData.hsPlayerName;
            highScore = saveData.highScore;
        }
    }

    private void OnTextChanged(string value)
    {
        playerName = value;
    }

    public string BestScoreChange()
    {
        return "Best Score : " + hsPlayerName + ": " + highScore;
    }
}
