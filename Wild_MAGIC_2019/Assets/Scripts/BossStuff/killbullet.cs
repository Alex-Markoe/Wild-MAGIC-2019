using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killbullet : MonoBehaviour
{
    float maxAge = 5;
    float age;

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        if(age >= maxAge)
        {
            Destroy(this.gameObject);
        }
    }
}
