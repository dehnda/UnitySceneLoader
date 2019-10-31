using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class HintManager : MonoBehaviour
{
    private Hints hints;

    [SerializeField]
    private string Filename;
    // Start is called before the first frame update
    void Start()
    {
        hints = new Hints();

        string path = Application.dataPath + "/Resources/" + Filename;

        if (!File.Exists(path))
        {
            var json = CreateDemoHints();
            WriteToFile(json, path);
        }
        else
        {
            var json = ReadFromFile(Filename);
            Debug.Log(json);
            JsonUtility.FromJsonOverwrite(json, hints);
        }

        Debug.Log(JsonUtility.ToJson(hints));
    }


    string ReadFromFile(string path) {
        var filePath = path.Replace(".json", "");
        TextAsset textFile = Resources.Load(filePath) as TextAsset;
        return textFile.text;
    }
    string CreateDemoHints()
    {
        var hint = new Hint();
        hint.duration = 0.0f;
        hint.text = "Enter your Hint here!";
        hint.title = "Provide title!";
        hints.items.Add(hint);
        hints.items.Add(hint);

        return JsonUtility.ToJson(hints, true);
    }

    void WriteToFile(string text, string path)
    {
        using (StreamWriter file = File.CreateText(path))
        {
            file.WriteAsync(text);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public class Hints {
    public List<Hint> items;

    public Hints() {
        items = new List<Hint>();
    }
}

[System.Serializable]
public class Hint
{
    public string title;
    public string text;
    public float duration;
}
