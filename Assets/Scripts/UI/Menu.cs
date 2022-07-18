using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    
    public void OpenPanel()
    {
        _menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePanel()
    {
        _menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}