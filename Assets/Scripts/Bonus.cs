using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    private GameObject paddle;

    private string[] bonusType = { "Life", "Speed", "Length", "Damage"};

    private int lifeUP, speedUP, lengthUP, damageUP;

    private int rotateDir = 1;

    // Start is called before the first frame update
    void Start()
    {
        paddle = GameObject.Find("Paddle");
    }

    // Update is called once per frame
    void Update()
    {
        //drop
        transform.position -= new Vector3(0, 3, 0) * Time.deltaTime;

        //rotation
        if (rotateDir ==1)
        {
            transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
            if(transform.eulerAngles.z >= 30 && transform.eulerAngles.z < 320)
            {
                rotateDir = -rotateDir;
            }
        }
        if (rotateDir == -1)
        {
            transform.Rotate(new Vector3(0, 0, -45) * Time.deltaTime);
            if (transform.eulerAngles.z <= 330 && transform.eulerAngles.z > 40)
            {
                rotateDir = -rotateDir;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            GetBonus();
            Destroy(this.gameObject);
        }
    }

    void GetBonus()
    {
        int bonus = Random.Range(0, bonusType.Length);
        switch (bonusType[bonus])
        {
            case "Life":
                UpLife();
                GameManager.gm.ShowBonus("Life");
                //Debug.Log("Life");
                break;
            case "Speed":
                SpeedUP();
                GameManager.gm.ShowBonus("Speed");
                //Debug.Log("Speed");
                break;
            case "Length":
                LengthUP();
                GameManager.gm.ShowBonus("Length");
                //Debug.Log("Length");
                break;
            case "Damage":
                DamageUP();
                GameManager.gm.ShowBonus("Damage");
                //Debug.Log("Damage");
                break;
        }
    }

    void UpLife()
    {
        lifeUP++;
        if (lifeUP < 2)
        {
            GameManager.gm.UpLife(2);
        }
        else if (lifeUP < 4)
        {
            GameManager.gm.UpLife(3);
        }
        else
        {
            GameManager.gm.UpLife(5);
        }
    }

    void SpeedUP()
    {
        speedUP++;
        if (speedUP < 5)
        {
            paddle.GetComponent<Paddle>().speed *= 1.06f;
        }
        else if (speedUP < 10)
        {
            paddle.GetComponent<Paddle>().speed *= 1.03f;
        }
        else
        {
            paddle.GetComponent<Paddle>().speed *= 1.01f;
        }
    } 

    void LengthUP()
    {
        lengthUP++;
        if (lengthUP < 5)
        {
            Vector3 transScale = new Vector3(paddle.transform.localScale.x * 1.08f, paddle.transform.localScale.y, paddle.transform.localScale.z);
            paddle.transform.localScale = transScale;
        }
        else if (lengthUP < 10)
        {
            Vector3 transScale = new Vector3(paddle.transform.localScale.x * 1.04f, paddle.transform.localScale.y, paddle.transform.localScale.z);
            paddle.transform.localScale = transScale;
        }
        else
        {
            Vector3 transScale = new Vector3(paddle.transform.localScale.x * 1.02f, paddle.transform.localScale.y, paddle.transform.localScale.z);
            paddle.transform.localScale = transScale;
        }          
    }

    void DamageUP()
    {
        damageUP++;
        if (damageUP < 3)
        {
            GameManager.gm.damage += 1;
            GameManager.gm.attackSpeed *= 0.9f;
        }
        else if (damageUP < 6)
        {
            GameManager.gm.damage += 2;
            GameManager.gm.attackSpeed *= 0.9f;
        }
        else
        {
            GameManager.gm.damage += 3;
            GameManager.gm.attackSpeed *= 0.9f;
        }
    }
}
