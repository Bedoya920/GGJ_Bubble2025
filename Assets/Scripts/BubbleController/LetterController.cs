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
            levelInfo = null;
        }

        private void LoadEventsFromFile(string path)
        {
            if (File.Exists(path))
            {
                string jsonContent = File.ReadAllText(path);
                letterPool = JsonUtility.FromJson<LetterObjectArray>(jsonContent).eventList;
                
            }
        }

        public void SetLevelInfo(int targetLevelID)
        {
           
            foreach (var letterList in letterPool)
            {
                if (letterList.levelID == targetLevelID)
                {
                    levelInfo = letterList;
                    return;
                }
            }

            levelInfo = null;
            Debug.LogWarning($"Level with ID {targetLevelID} not found in the letter pool.");

        }

        public void ShuffleLettersInLevel(LetterList level)
        {
            if (level == null || level.letters == null || level.letters.Count == 0)
            {
                Debug.LogWarning("The level or its letters are empty.");
                return;
            }

            var rng = new System.Random(); // Generador de números aleatorios
            for (int i = level.letters.Count - 1; i > 0; i--)
            {
                // Obtener un índice aleatorio
                int j = rng.Next(i + 1);

                // Intercambiar las posiciones de las letras
                (level.letters[i], level.letters[j]) = (level.letters[j], level.letters[i]);
            }
        }

        public void SubstractPoolLetter(string letter)
        {

            for (int i = 0; i < levelInfo.letters.Count; i++)
            {
                if (levelInfo.letters[i].letter == letter)
                {
                    var letterObject = levelInfo.letters[i];
                    letterObject.pool--;

                    if (letterObject.pool <= 0)
                    {
                        levelInfo.letters.RemoveAt(i);
                        return;
                    }
                    return;
                }
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
