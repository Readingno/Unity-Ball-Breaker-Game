using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Controller : MonoBehaviour
{
    public GameObject level_1;
    public GameObject level_2;
    public GameObject level_3;

    public List<GameObject> levels_d1;
    public List<GameObject> levels_d2;
    public List<GameObject> levels_d3;
    public List<GameObject> levels_d4;

    private GameObject currentLevel;

    private float levelTime;
    private int difficulty;
    private float levelTimeMax;
    private int clearScore;

    public GameObject timeBar;
    private enum LevelState { Start, Coming, Playing, Leaving, Finished};
    private LevelState levelState = LevelState.Start;

    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (levelState)
        {
            case LevelState.Start:
                levelTime = 0;
                LevelStart();
                break;
            case LevelState.Coming:
                ComeIn();
                break;
            case LevelState.Playing:
                levelTime += Time.deltaTime;
                TimeBar();
                CheckOver();
                break;
            case LevelState.Leaving:
                CheckOver();
                LevelOut();
                break;
            case LevelState.Finished:
                levelState = LevelState.Start;
                DestroyImmediate(currentLevel);
                Debug.Log("Score" + GameManager.gm.score);
                break;
        }
    }
    
    void LevelStart()
    {
        difficulty++;
        GameManager.gm.difficulty++;
        if (difficulty == 1)
        {
            levelTimeMax = 30;
            clearScore = 20;
            currentLevel = Instantiate(level_1, new Vector3(-10, 0, 0), Quaternion.identity);
        }
        else if (difficulty == 2)
        {
            levelTimeMax = 30;
            clearScore = 20;
            currentLevel = Instantiate(level_2, new Vector3(-10, 0, 0), Quaternion.identity);
        }
        else if (difficulty == 3)
        {
            levelTimeMax = 30;
            clearScore = 20;
            currentLevel = Instantiate(level_3, new Vector3(-10, 0, 0), Quaternion.identity);
        }
        else if (difficulty <= 6)
        {
            GameManager.gm.ballSpeed += 0.3f;
            GameManager.gm.UpLife(0.4f);
            music.pitch += 0.02f;
            levelTimeMax = 45;
            clearScore = 50;
            currentLevel = Instantiate(levels_d1[Random.Range(0, levels_d1.Count)], new Vector3(-10, 0, 0), Quaternion.identity);
        }
        else if (difficulty <= 10)
        {
            GameManager.gm.ballSpeed += 0.3f;
            GameManager.gm.UpLife(0.7f);
            music.pitch += 0.03f;
            levelTimeMax = 45;
            clearScore = 100;
            currentLevel = Instantiate(levels_d2[Random.Range(0, levels_d2.Count)], new Vector3(-10, 0, 0), Quaternion.identity);
        }
        else if (difficulty <= 15)
        {
            GameManager.gm.ballSpeed += 0.1f;
            music.pitch += 0.04f;
            levelTimeMax = 45;
            clearScore = 200;
            currentLevel = Instantiate(levels_d3[Random.Range(0, levels_d3.Count)], new Vector3(-10, 0, 0), Quaternion.identity);
        }
        else
        {
            music.pitch += 0.05f;
            levelTimeMax = 45;
            clearScore = difficulty * 10;
            currentLevel = Instantiate(levels_d4[Random.Range(0, levels_d4.Count)], new Vector3(-10, 0, 0), Quaternion.identity);
        }
        Debug.Log("Level " + difficulty);
        levelState = LevelState.Coming;
    }

    void ComeIn()
    {
        currentLevel.transform.position = Vector3.MoveTowards(currentLevel.transform.position, Vector3.zero, 5 * Time.deltaTime);
        if (currentLevel.transform.position == Vector3.zero)
        {
            levelState = LevelState.Playing;
        }
        
    }

    void TimeBar()
    {
        timeBar.transform.localScale = new Vector3(10 * (1 - levelTime / levelTimeMax), timeBar.transform.localScale.y, 1);
    }

    void CheckOver()
    {
        if (currentLevel != null)
        {
            if (currentLevel.GetComponentInChildren<BoxCollider2D>() == null)
            {
                GameManager.gm.ShowClear();
                GameManager.gm.AddScore(clearScore);
                levelState = LevelState.Finished;
            }
            else if(levelTime > levelTimeMax && levelState == LevelState.Playing)
            {
                levelState = LevelState.Leaving;
            }
        }
        
    }

    void LevelOut()
    {
        currentLevel.transform.position = Vector3.MoveTowards(currentLevel.transform.position, new Vector3(10, 0, 0), 5 * Time.deltaTime);
        if (currentLevel.transform.position == new Vector3(10, 0, 0))
        {
            levelState = LevelState.Finished;
        }
    }
}
