using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    
    public GameObject newball;
    public GameObject balls;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootBallTimer());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }

    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(speed * horizontalMove, 0, 0);
    }

    IEnumerator ShootBallTimer()
    {
        while (true)
        {
            Vector3 pos = rb.transform.position + new Vector3(0,0.2f,0);
            GameObject b = Instantiate(newball, pos, Quaternion.identity);
            b.transform.SetParent(balls.transform);
            if (GameManager.gm != null)
            {
                yield return new WaitForSeconds(GameManager.gm.attackSpeed);
            }
            else
            {
                yield return new WaitForSeconds(4);
            }
        }
    }
}
