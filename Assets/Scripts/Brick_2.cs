using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick_2 : MonoBehaviour
{
    public int hitPointMax;
    private int hitPoint;
    public GameObject hpBar;
    private float barLenth;

    public float shootSpeed;
    public GameObject newball;
    private GameObject balls;

    // Start is called before the first frame update
    void Start()
    {
        balls = GameObject.Find("Balls_Enemy");

        SetMaxHP();
        hitPoint = hitPointMax;
        barLenth = hpBar.transform.localScale.x;

        StartCoroutine(ShootBallTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetMaxHP()
    {
        if (GameManager.gm.difficulty < 5)
        {
            hitPointMax = 5;
        }
        else if (GameManager.gm.difficulty < 10)
        {
            hitPointMax = GameManager.gm.difficulty;
        }
        else if (GameManager.gm.difficulty < 20)
        {
            hitPointMax = 10 + GameManager.gm.difficulty / 4;
        }
        else if (GameManager.gm.difficulty < 30)
        {
            hitPointMax = 10 + GameManager.gm.difficulty / 3;
        }
        else
        {
            hitPointMax = 8 + GameManager.gm.difficulty / 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hitPoint -= GameManager.gm.damage;
            hpBar.transform.localScale = new Vector3((float)hitPoint / (float)hitPointMax * barLenth, 0.3f, 1);
            if (hitPoint <= 0)
            {
                Destroy(this.gameObject);
                GameManager.gm.AddScore(hitPoint + GameManager.gm.damage);
            }
            else
            {
                GameManager.gm.AddScore(GameManager.gm.damage);
            }
        }
    }

    IEnumerator ShootBallTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootSpeed / 3);
            Vector3 pos = this.transform.position + new Vector3(0, -0.2f, 0);
            GameObject b = Instantiate(newball, pos, Quaternion.identity);
            b.transform.SetParent(balls.transform);
            yield return new WaitForSeconds(shootSpeed / 3 * 2);
        }
    }
}
