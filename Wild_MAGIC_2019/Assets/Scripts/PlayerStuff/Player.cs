using System.Collections;
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

    [Space(15)]
    [Header("Player's Scripts")]
    public PlayerMovement pMove;

    [Space(15)]
    public GameObject swordObject;
    public GameObject createdSword;

    [Space(15)]
    [Header("PlayerUI")]
    public Sprite heart;

    private float swordTimer;

    // Start is called before the first frame update
    void Start()
    {
        pMove.movementSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (swordTimer > 0)
            swordTimer -= Time.deltaTime;
        else
            GameObject.Destroy(createdSword);

        if(Input.GetKey(KeyCode.Space) && swordTimer <= 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (createdSword != null)
            GameObject.Destroy(createdSword);

        // Start the sword timer
        swordTimer = attackTime;

        // Get the direction of the attack based off the player movement
        Vector3 direction = pMove.GetDirection();
        int x = 0, y = 0;

        // So we need to rotate the sword to be in that direction
        swordObject.transform.right = -direction.normalized;

        #region Sword Directions
        if (direction.x > 0)
        {
            x = -1;
            y = 0;
        }
        if(direction.x < 0)
        {
            x = 1;
            y = 0;
        }
        if (direction.y > 0)
        {
            x = 0;
            y = -1;
        }
        if (direction.y < 0)
        {
            x = 0;
            y = 1;
        }

        if (direction.x > 0 && direction.y > 0)
        {
            x = -1;
            y = -1;
        }
        if (direction.x < 0 && direction.y < 0)
        {
            x = 1;
            y = 1;
        }
        if (direction.x < 0 && direction.y > 0)
        {
            x = 1;
            y = -1;
        }
        if (direction.x > 0 && direction.y < 0)
        {
            x = -1;
            y = 1;
        }
        #endregion

        createdSword = Instantiate(swordObject, transform.position, Quaternion.FromToRotation(transform.right, new Vector3(x, y, 0)), transform);

        RaycastHit2D[] cols = Physics2D.RaycastAll(transform.position, direction, swordLength);
        foreach (RaycastHit2D col in cols)
        {
            if (col.transform.gameObject.tag == "Enemy")
            {
                Debug.Log("DAMAGE AN ENEMY");
                if (col.transform.GetComponent<EnemyBase>() != null)
                    col.transform.GetComponent<EnemyBase>().TakeDamage(1);
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

    public void DamageEnemy()
    {

    }

    public void OnGUI()
    {
        GUILayout.BeginArea(new Rect(30, 30, Screen.width, Screen.height));
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
