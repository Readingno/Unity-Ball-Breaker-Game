using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager gm;

    private float hpTimer;

    public int score;
    public Text scoreText;

    public List<Text> bonusText;
    public Text clearText;

    public GameObject bonus;
    public int[] bonusScore;
    private int bonusIndex;

    private float lifeMax = 5;
    private float life = 5;
    public GameObject healthBar;

    public int damage = 3;
    public float attackSpeed = 6;
    public int difficulty;
    public float ballSpeed = 7;

    public GameObject gameOver; 

    public enum GameState { Playing, GameOver};
    public GameState gameState = GameState.Playing;

    private void Awake()
    {
        if(gm != null)
        {
            Destroy(gameObject);
        }
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
        //hpTimer += Time.deltaTime;

        Regeneration();
        ShowHealthBar();
        CheckState();

        CreateBonus();
        ShowScore();
    }

    void CheckState()
    {
        switch (gameState)
        {
            case GameState.Playing:
                if (life <= 0)
                {
                    gameState = GameState.GameOver;
                }
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }
    }

    public void LoseHP()
    {
        life --;
    }

    void Regeneration()
    {
        life += lifeMax / 300 * Time.deltaTime;
        if (life > lifeMax)
        {
            life = lifeMax;
        }
    }

    void ShowHealthBar()
    {
        healthBar.transform.localScale = new Vector3(life / lifeMax, healthBar.transform.localScale.y, 1);
    }

    public void UpLife(float x)
    {
        lifeMax += x;
        life += x; 
    }

    public void AddScore(int x)
    {
        score += x;
    }

    void ShowScore()
    {
        scoreText.text = score.ToString().PadLeft(5, '0');
    }

    public void ShowBonus(string type)
    {
        StartCoroutine(ShowBonusTimer(type));
    }

    IEnumerator ShowBonusTimer(string type)
    {
        scoreText.enabled = false;
        int t = 0;
        switch (type)
        {
            case "Life":
                t = 0;
                break;
            case "Damage":
                t = 1;
                break;
            case "Speed":
                t = 2;
                break;
            case "Length":
                t = 3;
                break;
        }
        bonusText[t].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        bonusText[t].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        bonusText[t].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        bonusText[t].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        bonusText[t].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        bonusText[t].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        scoreText.enabled = true;
    }

    public void ShowClear()
    {
        StartCoroutine(ShowClearTimer());
    }

    IEnumerator ShowClearTimer()
    {
        scoreText.enabled = false;
        clearText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        clearText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        clearText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        clearText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        scoreText.enabled = true;
    }

    void CreateBonus()
    {
        if (score > bonusScore[bonusIndex])
        {
            bonusIndex++;
            Vector3 pos = new Vector3(Random.Range(-4f, 4f), 8.5f, 0);
            Instantiate(bonus, pos, Quaternion.identity);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
    }
}
