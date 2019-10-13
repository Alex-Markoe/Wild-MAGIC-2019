using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    private Vector3 position;
    private float timer;
    private float shift;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        timer = 0f;
        shift = .1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            position.y += shift;
            shift = -shift;
            timer = 0;
        }

        SetTransform();
    }
    void SetTransform()
    {
        transform.position = position;
    }
}
