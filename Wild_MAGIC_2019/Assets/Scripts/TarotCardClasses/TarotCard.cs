using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Sun,
    Death,
    Moon,
    None
}
public class TarotCard : MonoBehaviour
{
    public Vector3 position;
    public CardType type;
    public bool chosen;
    private BoxCollider2D collider;
    public GameObject imagesTextPrefab;
    private GameObject imagesText;

    public Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        chosen = false;
        collider = GetComponent<BoxCollider2D>();
        myCamera = Camera.main;
        imagesText = Instantiate(imagesTextPrefab);
        imagesText.gameObject.transform.position = new Vector3(-100, -100, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isHovered())
        {
            imagesText.transform.position = new Vector3(collider.bounds.center.x, collider.bounds.center.y, 1);
        }
        else
        {
            imagesText.transform.position = new Vector3(-100, 100, 0);
        }
        SetTransform();

    }

    void SetTransform()
    {
        transform.position = position;
    }

    private void OnMouseDown()
    {
        if (type == CardType.Sun)
        {
            Debug.Log("Your greed for power has reduced your light!");
        }
        else if(type == CardType.Death)
        {
            Debug.Log("It's time for your rebirth with some new found power!");
        }
        else
        {
            Debug.Log("You have taken a risk relying on light!");
        }

        chosen = true;
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
