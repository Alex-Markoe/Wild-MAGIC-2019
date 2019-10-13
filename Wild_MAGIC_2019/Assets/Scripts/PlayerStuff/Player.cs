﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Settings")]
    public float hp = 3f;
    public float movementSpeed = 3f;
    public float attackDamage = 1f;
    public float attackTime = 0.05f;
    public float swordLength = 2;
    public bool dashing = false;
    public float lightRadius = 2;
    public Transform lightMask;

    [Space(15)]
    [Header("Player's Scripts")]
    public PlayerMovement pMove;

    [Space(15)]
    public GameObject swordObject;
    private GameObject createdSword;

    [Space(15)]
    [Header("PlayerUI")]
    public Sprite heart;

    private float swordTimer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        pMove.movementSpeed = movementSpeed;
    }

    void SetLightRadius(float amt)
    {
        lightRadius = amt;
        lightMask.localScale = new Vector3(lightRadius, lightRadius, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (swordTimer > 0)
            swordTimer -= Time.deltaTime;
        else
        {
            GameObject.Destroy(createdSword);
            pMove.attacking = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && swordTimer <= 0)
        {
            Attack();
            pMove.attacking = true;
        }
    }

    void Attack()
    {
        if(dashing)
        {

        }
        if (createdSword != null)
            GameObject.Destroy(createdSword);

        // Start the sword timer
        swordTimer = attackTime;

        // Get the direction of the attack based off the player movement
        Vector3 direction = pMove.GetDirection();
        int x = 0, y = 0;

        float directionAngle = 0;

        #region Sword Directions
        if (direction.x > 0)
        {
            x = -1;
            y = 0;
            directionAngle = 0f;
        }
        if (direction.x < 0)
        {
            x = 1;
            y = 0;

            directionAngle = 180f;
        }
        if (direction.y > 0)
        {
            x = 0;
            y = -1;

            directionAngle = 90f;
        }
        if (direction.y < 0)
        {
            x = 0;
            y = 1;

            directionAngle = 270f;
        }

        if (direction.x > 0 && direction.y > 0)
        {
            x = -1;
            y = -1;

            directionAngle = 45f;
        }
        if (direction.x < 0 && direction.y < 0)
        {
            x = 1;
            y = 1;


            directionAngle = 225f;
        }
        if (direction.x < 0 && direction.y > 0)
        {
            x = 1;
            y = -1;

            directionAngle = 135f;
        }
        if (direction.x > 0 && direction.y < 0)
        {
            x = -1;
            y = 1;

            directionAngle = 315f;
        }
        #endregion

        createdSword = Instantiate(swordObject, transform.position, Quaternion.FromToRotation(transform.right, -new Vector3(x, y, 0)), transform);

        float arcAccuracy = 0.1f;
        float arcSize = 60;
        for (float i = -(arcSize / 2); i < arcSize/2; i += arcAccuracy)
        {
            float angle = (directionAngle + i) * Mathf.Deg2Rad;

            Vector3 rot = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

            Debug.DrawRay(transform.position, rot.normalized * swordLength, Color.red / 4);

            RaycastHit2D[] cols = Physics2D.RaycastAll(transform.position, rot, swordLength);
            foreach (RaycastHit2D col in cols)
            {
                if (col.transform.gameObject.tag == "Enemy")
                {
                    if (col.transform.GetComponent<EnemyBase>() != null)
                        col.transform.GetComponent<EnemyBase>().TakeDamage(1);
                    i = arcSize / 2;
                }
            }
        }
    }
    public void TakeDamage(float amt)
    {
        hp -= amt;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    void Die()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        for (int i = 0; i < hp; i++)
        {
            GUILayout.Label(heart.texture);
        }
        GUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
