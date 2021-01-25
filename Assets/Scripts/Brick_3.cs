using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick_3 : MonoBehaviour
{
    public int hitPointMax;
    private int hitPoint;
    public GameObject hpBar;
    private float barLenth;

    // Start is called before the first frame update
    void Start()
    {
        SetMaxHP();
        hitPoint = hitPointMax;
        barLenth = hpBar.transform.localScale.x;
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
        if (collision.gameObject.CompareTag("Ball") && (collision.gameObject.transform.position.y - transform.position.y > 0.1))
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
}
