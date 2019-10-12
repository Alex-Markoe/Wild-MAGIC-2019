using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 3f;

    private Rigidbody2D rb;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        else
            direction = Vector3.zero;

        if (direction.magnitude > 1)
            direction = direction.normalized;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (direction * movementSpeed * Time.deltaTime));
    }
}
