using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    public GameObject menuCanva;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.SetActive(false);
            menuCanva.SetActive(true);
        }
    }
}
