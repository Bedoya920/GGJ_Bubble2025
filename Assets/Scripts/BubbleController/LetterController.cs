using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SignalSystem
{
    public class LetterController : MonoBehaviour
    {
        [Header("Letter information")]
        public LetterList[] letterPool;
        public LetterList levelInfo;

        void Start()
        {
            //LoadEventsFromFile("Assets/Data/signals.json");
            string path = Path.Combine(Application.streamingAssetsPath, "events.json");
            LoadEventsFromFile(path);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void LoadEventsFromFile(string path)
        {
            if (File.Exists(path))
            {
                string jsonContent = File.ReadAllText(path);
                letterPool = JsonUtility.FromJson<LetterObjectArray>(jsonContent).eventList;
            }
        }
    }

    [System.Serializable]
    public class LetterObjectArray
    {
        public LetterList[] eventList;
    }

    [System.Serializable]
    public class LetterList
    {
        public int levelID;
        public List<LetterObject> letters;
    }

    [System.Serializable]
    public class LetterObject
    {
        public string letter;
        public string position;
        public int pool;
    }

}
