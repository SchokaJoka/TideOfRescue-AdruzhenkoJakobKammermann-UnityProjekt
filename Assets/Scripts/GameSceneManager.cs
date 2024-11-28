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
    private string GameWon = "GameWon";
    
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

    public void LoadGameWonScene()
    {
        SceneManager.LoadScene(GameWon);
    }
}
