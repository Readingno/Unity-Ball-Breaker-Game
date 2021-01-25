using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick2_Mainmenu : MonoBehaviour
{
    public float shootSpeed;
    public GameObject newball;
    private GameObject balls;

    // Start is called before the first frame update
    void Start()
    {
        balls = GameObject.Find("Balls_Enemy");

        StartCoroutine(ShootBallTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ShootBallTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootSpeed/2);
            Vector3 pos = this.transform.position + new Vector3(0, -0.2f, 0);
            GameObject b = Instantiate(newball, pos, Quaternion.identity);
            b.transform.SetParent(balls.transform);
            yield return new WaitForSeconds(shootSpeed/2);
        }
    }
}
