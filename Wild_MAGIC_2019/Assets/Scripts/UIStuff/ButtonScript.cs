using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public int indexOfTransition;

    private BoxCollider2D collider;
    private Camera myCamera;
    private SpriteRenderer sprite;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        myCamera = Camera.main;

        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHovered())
        {
            sprite.color = Color.red;
        }
        else
        {
            sprite.color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        source.Play();
        if (indexOfTransition == -1)
        {
            Debug.Log("Closed");
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(indexOfTransition);
        }
    }

    private bool isHovered()
    {
        Vector3 mousePosition = myCamera.ScreenToWorldPoint(Input.mousePosition);
        if ((mousePosition.x > collider.bounds.min.x && mousePosition.x < collider.bounds.max.x) &&
            (mousePosition.y > collider.bounds.min.y && mousePosition.y < collider.bounds.max.y))
        {
            return true;
        }
        return false;
    }
}
