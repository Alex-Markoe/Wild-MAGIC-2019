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
    public float flickerSpeed = 0.25f;
    private float flicker;
    public Rigidbody2D rb;
    public bool invul = false;
    public float invulTimer;
    private float invulMax = .5f;
    private float invulFlicker = .1f;
    public Color color;
    public float lightTimer;
    public float lightMax = 20f;
    public bool lightShrink;
    public GameManager gameManager;
    public RoomManager roomManager;

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

    // Start is called before the first frame update
    void Start()
    {
        pMove.movementSpeed = movementSpeed;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
        lightShrink = true;
        lightTimer = lightMax;
    }

    public void SetLightRadius(float amt)
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

        if (Input.GetKeyDown(KeyCode.Space) && swordTimer <= 0)
        {
            Attack();
            pMove.attacking = true;
            pMove.dashing = dashing;
        }

        if (flicker <= 0)
        {
            float flickerScale = Random.Range(lightRadius, lightRadius + 0.2f);
            lightMask.localScale = Vector3.Lerp(lightMask.localScale, new Vector3(flickerScale, flickerScale), 10f * Time.deltaTime);
            flicker = flickerSpeed;
        }
        else
        {
            flicker -= Time.deltaTime;
        }

        if (invulTimer > invulFlicker * 5 && invulTimer > 0 && invul)
        {
            invulTimer -= Time.deltaTime;
            color = GetComponentInChildren<SpriteRenderer>().color;
            color.a = .5f;
            GetComponentInChildren<SpriteRenderer>().color = color;
        }
        else if (invulTimer > invulFlicker * 4 && invulTimer > 0 && invul)
        {
            invulTimer -= Time.deltaTime;
            color = new Color(1, 1, 1);
            GetComponentInChildren<SpriteRenderer>().color = color;
        }
        else if (invulTimer > invulFlicker * 3 && invulTimer > 0 && invul)
        {
            invulTimer -= Time.deltaTime;
            color = GetComponentInChildren<SpriteRenderer>().color;
            color.a = .5f;
            GetComponentInChildren<SpriteRenderer>().color = color;
        }
        else if (invulTimer > invulFlicker * 2 && invulTimer > 0 && invul)
        {
            invulTimer -= Time.deltaTime;
            color = new Color(1, 1, 1);
            GetComponentInChildren<SpriteRenderer>().color = color;
        }
        else if (invulTimer > invulFlicker && invulTimer > 0 && invul)
        {
            invulTimer -= Time.deltaTime;
            color = GetComponentInChildren<SpriteRenderer>().color;
            color.a = .5f;
            GetComponentInChildren<SpriteRenderer>().color = color;
        }
        else if (invul)
        {
            invul = false;
            invulTimer = invulMax;
            color = new Color(1, 1, 1);
            GetComponentInChildren<SpriteRenderer>().color = color;
        }
        if (!roomManager.currentRoom.firstClear)
        {
            if (lightTimer > gameManager.currentLight && lightShrink)
            {
                lightTimer -= Time.deltaTime * 2;
                if (lightTimer > gameManager.currentLight)
                {
                    SetLightRadius(lightTimer);
                }
                else
                {
                    SetLightRadius(gameManager.currentLight);
                }
                lightTimer -= Time.deltaTime;
            }
            else if (lightShrink)
            {
                lightShrink = false;
                lightTimer = lightMax;
            }
        }
        else
        {
            SetLightRadius(20);
        }
    }

    public void Attack()
    {
        if(dashing)
        {
            pMove.Dash();
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
                if (col.transform.gameObject.tag == "Enemy" || col.transform.gameObject.tag == "Boss")
                {
                    if (col.transform.GetComponent<EnemyBase>() != null)
                        col.transform.GetComponent<EnemyBase>().TakeDamage(1);

                    if (col.transform.GetComponent<Boss>() != null)
                        col.transform.GetComponent<Boss>().TakeDamage(1);

                    pMove.dashTimer = -1;
                    pMove.dashing = false;

                    Debug.Log("Hit");

                    i = 60f;
                }
            }
        }
    }
    public void TakeDamage(float amt)
    {
        if (!invul)
        {
            hp -= amt;
            Camera.main.GetComponent<ScreenShake>().Shake();
            invul = true;
        }

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
