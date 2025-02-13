using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitMenu : MonoBehaviour
{
    public GameObject menuCanva;
    public LevelManager levelManager;
    int currentScene;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentScene == 0)
            {
                QuitGame();
            } else{
                GoMenu();
            }
            
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BackMenu();
        }
    }

    public void QuitGame()
    {
        levelManager.QuitGame();
    }

    public void BackMenu()
    {
        menuCanva.SetActive(true);
        gameObject.SetActive(false);
    }

    public void GoMenu()
    {
        levelManager.Menu();
    }

    


}
