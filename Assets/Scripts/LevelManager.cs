using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{

    [SerializeField] private float sceneLoadDelay = 1.5f;
    private ScoreKeeper _scoreKeeper;

    void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    
    public void LoadGame()
    {
        _scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GameOver()
    {
        StartCoroutine(WaitAndLoad("Game Over", sceneLoadDelay));
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneNme, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Game Over");
    }
}
