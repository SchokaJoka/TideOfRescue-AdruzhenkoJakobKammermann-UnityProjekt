using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private string GameOver = "GameOver";
    private string GameScene = "GameScene";
    private string MainMenu = "MainMenu";
    
    public void StartGameScene()
    {
        SceneManager.LoadScene(GameScene);
    }
    
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(GameOver);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(MainMenu);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}