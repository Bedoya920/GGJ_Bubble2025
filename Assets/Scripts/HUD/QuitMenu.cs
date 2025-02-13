using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    public GameObject menuCanva;
    public LevelManager levelManager;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            QuitGame();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BackMenu();
        }
    }

    public void QuitGame()
    {
        levelManager.QuitGame();
        print("Se salió");
    }

    public void BackMenu()
    {
        
        menuCanva.SetActive(true);
        print("Back menu");
        gameObject.SetActive(false);
    }


}
