using System.Collections.Generic;

namespace SceneLoadingSystem
{

    /// <summary>
    /// Class representation of a single hint. Can be serialized.
    /// </summary>
    [System.Serializable]
    public class Hint
    {
        public string title;
        public string text;
        public float duration;
    }

    /// <summary>
    /// Class representation of a lists of hints. Can be serialized.
    /// </summary>
    [System.Serializable]
    public class Hints
    {
        public List<Hint> items;

        public Hints()
        {
            items = new List<Hint>();
        }
    }
}