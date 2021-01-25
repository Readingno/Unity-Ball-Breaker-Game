using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = GameManager.gm.score.ToString().PadLeft(5, '0');
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.gm.gameState = GameManager.GameState.Playing;
        Debug.Log("Restart");
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        GameManager.gm.gameState = GameManager.GameState.Playing;
        Debug.Log("MainMenu");
    }
}
