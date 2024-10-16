using UnityEngine;

public class ButtonsManager : MonoBehaviour
{

    private void Start()
    {
        AudioController.ExitApp.AddListener(ExitApp);
    }

    private void ExitApp()
    {
        Application.Quit();
    }

}