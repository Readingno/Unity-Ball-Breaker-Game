using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Paddle")
        {
            float hitOffset = rb.transform.position.x - collision.transform.position.x;
            if (GameManager.gm != null)
            {
                rb.velocity = new Vector3(hitOffset / collision.collider.bounds.size.x , 0.3f, 0).normalized * GameManager.gm.ballSpeed;
            }
            else
            {
                rb.velocity = new Vector3(hitOffset / collision.collider.bounds.size.x, 0.3f, 0).normalized * 8;
            }
        }
    }
}
