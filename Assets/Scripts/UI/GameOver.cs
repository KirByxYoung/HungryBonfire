using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Bonfire _bonfire;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _panel;

    private void OnEnable()
    {
        _bonfire.Died += OpenPanel;
    }

    private void OnDisable()
    {
        _bonfire.Died -= OpenPanel;
    }

    private void OpenPanel()
    {
        _panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        _bonfire.Restart();
        _player.Restart();
        _panel.SetActive(false);
        Time.timeScale = 1;
    }
}
