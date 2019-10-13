using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    public float maxShakeTime = 0.1f;
    public float shakeSize = 0.25f;

    private float shake;
    public Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(shake > 0)
        {
            transform.position = new Vector3(Random.Range(startPos.x - shakeSize, startPos.x + shakeSize), Random.Range(startPos.y - shakeSize, startPos.y + shakeSize), startPos.z);
            shake -= Time.deltaTime;
        }
        else
        {
            transform.position = startPos;
        }
    }

    public void Shake()
    {
        startPos = transform.position;
        shake = maxShakeTime;
    }
}
