using System.Collections.Generic;

namespace SceneLoadingSystem
{

     [System.Serializable]
    public class Hint
    {
        public string title;
        public string text;
        public float duration;
    }

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