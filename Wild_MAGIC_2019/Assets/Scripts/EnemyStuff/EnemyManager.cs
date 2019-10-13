using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyHard;
    public GameObject enemyShooter;
    public GameObject player;
    public EnemyMovement[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindObjectsOfType<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
