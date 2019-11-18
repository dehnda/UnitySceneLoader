using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

namespace SceneLoadingSystem
{
    /// <summary>
    /// Class for managing hints
    /// Contains all methods for loading and saving hints.
    /// </summary>
    /// <remarks>
    /// This class can get a Hint, get a hint duration and set filename.
    /// </remarks>
    public class HintManager : MonoBehaviour
    {
        private Hints hints;
        [SerializeField]
        public string Filename { get; set; } = "hints.json";
        private int currentHintIndex = 0;
        private string path;

        // Start is called before the first frame update
        private void Start()
        {
            if (Filename == "") Debug.LogError("Filename should not be empty");

            path = $"{Application.dataPath}/Resources/";

            hints = new Hints();

            CreateRessourceFolder();
            CreateHintFile();

            Debug.Assert(hints.items.Count > 0, "SceneLoader: HintList is empty!");
        }
        /// <summary>
        /// Creates "Resources" folder in Application.dataPath
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        /// <exception cref="System.IO.IOException">Thrown when the folder 
        /// cannot be created.</exception>
        private void CreateRessourceFolder()
        {
            var pathInfo = new DirectoryInfo(path);

            if (pathInfo.Exists)
                return;
        
            try
            {
                pathInfo.Create();
                Debug.Log("Folder " + path + " created.");
            }
            catch (System.IO.IOException)
            {
                Debug.LogError($"{this.name}: Folder {path} could not be created.");
            }

        

        }
        /// <summary>
        /// Creates "Hint" file in Application.dataPath
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        private void CreateHintFile()
        {
            if (!File.Exists(path + Filename))
            {
                var json = CreateDemoHints();
                WriteToFile(json);
            }
            else
            {
                var json = ReadFromFile();
                JsonUtility.FromJsonOverwrite(json, hints);
            }
        }

        /// <summary>
        /// Reads hints from Ressoure hint json.
        /// </summary>
        /// <returns>
        /// The hint list as json.
        /// </returns>
        private string ReadFromFile()
        {
            TextAsset textFile = Resources.Load<TextAsset>(Filename.Replace(".json", ""));
            if (textFile != null)
            {
                return textFile.text;
            } else {
                Debug.LogError($"HintManager: Resource {Filename} could not be loaded.");
                return "";
            }
        }

        /// <summary>
        /// Creates two default hints and adds to hint list.
        /// </summary>
        /// <returns>
        /// The the list with two default hints as json.
        /// </returns>
        private string CreateDemoHints()
        {
            var hint = new Hint();
            hint.duration = 1.0f;
            hint.text = "Enter your Hint here!";
            hint.title = "Provide title!";
            hints.items.Add(hint);
            hints.items.Add(hint);

            return JsonUtility.ToJson(hints, true);
        }

        /// <summary>
        /// Writes a string in to hint file.
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        /// <exception cref="System.Exception">Thrown when WriteAsync
        /// fails.</exception>
        /// <param name="text">A string to append to file.</param>
        private void WriteToFile(string text)
        {
            using (StreamWriter file = File.CreateText(path + Filename))
            {
                try
                {
                    file.WriteAsync(text);
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"{this.name}: Resource {path}\\{Filename} could not be written.\n" +
                                   $"Exception Message: {e.Message}");
                }

            }
        }

        /// <summary>
        /// Getter for next hint.
        /// </summary>
        /// <returns>
        /// The next Random hint of the list.
        /// </returns>
        public string GetNextHint()
        {
            currentHintIndex = UnityEngine.Random.Range(0, hints.items.Count);
            return hints.items[currentHintIndex].text;
        }

        /// <summary>
        /// Getter for current hint duration.
        /// </summary>
        /// <returns>
        /// The current hint duration as float [sec].
        /// </returns>
        public float GetHintTime()
        {
            return hints.items[currentHintIndex].duration;
        }
    }
}