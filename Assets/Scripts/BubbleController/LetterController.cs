using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

namespace SignalSystem
{
    public class LetterController : MonoBehaviour
    {
        [Header("Letter information")]
        public LetterList[] letterPool;
        public LetterList levelInfo;
        public int total;
        public bool isLoaded;

        [Header("Level information")]
        public LevelManager levelManager;
        public TextController textController;

        private void Awake()
        {
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
                isLoaded = true;               
            }
        }

        public void SetLevelInfo(int targetLevelID)
        {
            Debug.Log(targetLevelID);
            Debug.Log("Longitud array" + letterPool.Length);
            foreach (var letterList in letterPool)
            {
                if (letterList.levelID == targetLevelID)
                {
                    levelInfo = letterList;
                    total = levelInfo.total;
                    textController.remainingValue.text = total.ToString();
                    Debug.Log("Encontro el listado del nivel");
                    return;
                }
            }

            levelInfo = null;
            Debug.Log("No encontro el listado del nivel");
            Debug.LogWarning($"Level with ID {targetLevelID} not found in the letter pool.");

        }

        public void ShuffleLettersInLevel()
        {
            if (levelInfo == null || levelInfo.letters == null || levelInfo.letters.Count == 0)
            {
                Debug.LogWarning("The level or its letters are empty.");
                return;
            }

            var rng = new System.Random(); // Generador de números aleatorios
            for (int i = levelInfo.letters.Count - 1; i > 0; i--)
            {
                // Obtener un índice aleatorio
                int j = rng.Next(i + 1);

                // Intercambiar las posiciones de las letras
                (levelInfo.letters[i], levelInfo.letters[j]) = (levelInfo.letters[j], levelInfo.letters[i]);
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

        public int GetLetterCount()
        {
            return levelInfo.letters.Count;
        }

        public void NextLevel()
        {
            if (levelInfo.letters.Count == 0)
            {
                levelManager.NextScene();
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
        public int total;
        public List<LetterObject> letters;
    }

    [System.Serializable]
    public class LetterObject
    {
        public string letter;
        public string position;
        public int pool;
        public bool specialCharacter; 
    }

}
