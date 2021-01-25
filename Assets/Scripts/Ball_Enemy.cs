using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;

    public GameObject hpBar;
    private int hitPoint;
    public int hitPointMax;
    private float barLenth;

    // Start is called before the first frame update
    void Start()
    {

        hitPoint = hitPointMax;
        barLenth = hpBar.transform.localScale.y;

        LaunchBall();
    }

    // Update is called once per frame
    void Update()
    {
        BallDrop();
        if (rb.velocity == Vector2.zero)
        {
            Destroy(this.gameObject);
        }
    }

    void LaunchBall()
    {
        if (GameManager.gm != null)
        {
            rb.velocity = new Vector3(Random.Range(-5, 5), Random.Range(-1, -20), 0).normalized * GameManager.gm.ballSpeed;
        }
        else
        {
            rb.velocity = new Vector3(Random.Range(-5, 5), Random.Range(-1, -20), 0).normalized * 8;
        }
    }

    void BallDrop()
    {
        if (rb.transform.position.y < -8)
        {
            //Debug.Log("Drop !");
            Destroy(this.gameObject);
            if (GameManager.gm != null)
            {
                if (this.CompareTag("Ball_Enemy"))
                {
                    GameManager.gm.LoseHP();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Paddle")
        {
            float hitOffset = rb.transform.position.x - collision.transform.position.x;
            if (GameManager.gm != null)
            {
                rb.velocity = new Vector3(hitOffset / collision.collider.bounds.size.x, 0.3f, 0).normalized * GameManager.gm.ballSpeed;
            }
            else
            {
                rb.velocity = new Vector3(hitOffset / collision.collider.bounds.size.x, 0.3f, 0).normalized * 8;
            }

            if (hitPoint > 0)
            {
                hitPoint--;
                hpBar.transform.localScale = new Vector3((float)hitPoint / (float)hitPointMax * barLenth, 0.3f, 1);
            }

            if (hitPoint <= 0)
            {
                this.tag = "Ball";
            }
        }
    }
}
