using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTimerOne : MonoBehaviour
{
    public bool first;
    public float cutsceneTimer;
    public float cutsceneMax = 3f;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        cutsceneTimer = cutsceneMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(cutsceneTimer> 0)
        {
            cutsceneTimer -= Time.deltaTime;
        }
        else
        {
            gameManager.StartCutscene();
        }
    }
}
