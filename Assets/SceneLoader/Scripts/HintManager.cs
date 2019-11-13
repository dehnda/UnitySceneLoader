using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

namespace SceneLoadingSystem
{

    public class HintManager : MonoBehaviour
    {
        private Hints hints;
        [SerializeField]
        private string Filename = "hints.json";
        private int hintIndex = 0;
        // Start is called before the first frame update
        void Start()
        {
            if (Filename == "") Debug.LogError("Filename should not be empty");

            hints = new Hints();

            CreateFolderAndHintFile();
        }

        private void CreateFolderAndHintFile()
        {
            var path = $"{Application.dataPath}/Resources/";

            try
            {
                var pathInfo = new DirectoryInfo(path);

                if (!pathInfo.Exists)
                {
                    pathInfo.Create();
                    Debug.Log("Folder " + path + " created.");
                }

                if (!File.Exists(path + Filename))
                {
                    var json = CreateDemoHints();
                    WriteToFile(json, path + Filename);
                }
                else
                {
                    var json = ReadFromFile(path + Filename);
                    JsonUtility.FromJsonOverwrite(json, hints);
                }

            }
            catch (Exception e)
            {
                throw e;
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

            hintIndex = UnityEngine.Random.Range(0, hints.items.Count);
            return hints.items[hintIndex].text;
        }

        public float GetHintTime()
        {
            return hints.items[hintIndex].duration;
        }

        public void SetFileName(string filename)
        {
            Filename = filename;
        }
    }
}