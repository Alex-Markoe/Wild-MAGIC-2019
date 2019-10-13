using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    private Vector3 position;
    private Vector3 direction;
    public Vector3 velocity;

    public float timer;

    private float randomTimer;

    public Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.up;

        myCamera = Camera.main;

        velocity = direction * .005f;
        position = transform.position;

        timer = Random.Range(0, 1.1f);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= .2)
        {
            velocity = direction * .002f;
        }
        if(timer >= .6 && timer <= 1)
        {
            velocity = direction * .002f;
        }
        if (timer >= .3 && timer < .6)
        {
            velocity = direction * .005f;
        }
        if (timer >= 1)
        {
            timer = 0;
            direction = -direction;
            velocity = direction * .005f;
        }
        position += velocity;
        SetTransform();
    }

    void SetTransform()
    {
        transform.position = position;
    }
}
