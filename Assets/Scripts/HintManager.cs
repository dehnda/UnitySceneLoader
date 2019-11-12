using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SceneLoadingSystem;


public class HintManager : MonoBehaviour
{
    private Hints hints;
    [SerializeField]
    private string Filename = "";
    private int hintIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (Filename == "") Debug.LogError("Filename should not be empty");

        hints = new Hints();

        string path = $"{Application.dataPath}/Resources/{Filename}";

        if (!File.Exists(path))
        {
            var json = CreateDemoHints();
            WriteToFile(json, path);
        }
        else
        {
            var json = ReadFromFile(Filename);
            JsonUtility.FromJsonOverwrite(json, hints);
        }

        Debug.Assert(hints.items.Count > 0, "SceneLoader: HintList is empty!");
    }


    string ReadFromFile(string path)
    {
        var filePath = path.Replace(".json", "");
        TextAsset textFile = Resources.Load(filePath) as TextAsset;
        return textFile.text;
    }
    string CreateDemoHints()
    {
        var hint = new Hint();
        hint.duration = 1.0f;
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

    public string GetNextHint()
    {

        hintIndex = Random.Range(0, hints.items.Count);
        return hints.items[hintIndex].text;
    }

    public float GetHintTime()
    {
        return hints.items[hintIndex].duration;
    }
}
