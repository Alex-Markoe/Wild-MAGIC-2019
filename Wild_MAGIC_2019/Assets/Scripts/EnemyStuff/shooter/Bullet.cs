using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    public GameObject player;
    private float movementSpeed = 7f;
    public Rigidbody2D rb;
    public GameObject enemy;
    public GameObject enemyHard;
    public GameObject enemyShooter;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 toTarget = player.transform.position - transform.position;
        toTarget = toTarget.normalized;
        angle = Mathf.Rad2Deg*(Mathf.Atan2(toTarget.y, toTarget.x));

        Debug.Log(angle);

        if((22.5f >= angle && angle >= 0) || (angle < 0 && -22.5f <= angle))
        {
            direction = new Vector3(1, 0, 0);
        }
        else if ((67.5 >= angle && angle > 22.5f))
        {
            direction = new Vector3(1, 1, 0);
        }
        else if ((112.5f >= angle && angle > 67.5f))
        {
            direction = new Vector3(0, 1, 0);
        }
        else if ((157.5 >= angle && angle > 112.5f))
        {
            direction = new Vector3(-1, 1, 0);
        }
        else if ((180 >= angle && angle > 157.5f) || (-180 <= angle && angle < -157.5f))
        {
            direction = new Vector3(-1, 0, 0);
        }
        else if ((-157.5f <= angle && angle < -112.5f))
        {
            direction = new Vector3(-1, -1, 0);
        }
        else if ((-112.5f <= angle && angle < -67.5f))
        {
            direction = new Vector3(0, -1, 0);
        }
        else if ((-67.5 <= angle && angle < -22.5f))
        {
            direction = new Vector3(1, -1, 0);
        }
        if (direction.magnitude > 1)
            direction = direction.normalized;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //   deletes on collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            if (collision.gameObject.tag == "Player")
                player.GetComponent<Player>().TakeDamage(1);

            Destroy(gameObject);
        }
    }
}
