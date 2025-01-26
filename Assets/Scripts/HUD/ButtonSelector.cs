using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour
{
    public Button defaultButton; // Asigna tu botón en el Inspector

    void Start()
    {
        // Asegúrate de que haya un EventSystem en la escena
        EventSystem eventSystem = EventSystem.current;

        if (eventSystem != null && defaultButton != null)
        {
            // Establecer el botón como seleccionado por defecto
            eventSystem.SetSelectedGameObject(defaultButton.gameObject);
        }
    }
}