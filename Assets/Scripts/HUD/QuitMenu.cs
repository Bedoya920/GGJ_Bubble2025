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
            levelManager.Menu();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.SetActive(false);
            menuCanva.SetActive(true);
        }
    }
}
