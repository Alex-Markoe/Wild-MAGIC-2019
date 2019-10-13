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
    private BoxCollider2D col;
    public GameObject imagesTextPrefab;
    private GameObject imagesText;

    private Camera myCamera;

    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        chosen = false;
        col = GetComponent<BoxCollider2D>();
        myCamera = Camera.main;
        imagesText = Instantiate(imagesTextPrefab);
        imagesText.gameObject.transform.position = new Vector3(-100, -100, 0);

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isHovered())
        {
            imagesText.transform.position = new Vector3(col.bounds.center.x, col.bounds.center.y, 1);
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
        source.Play();
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
        if ((mousePosition.x > col.bounds.min.x && mousePosition.x < col.bounds.max.x) &&
            (mousePosition.y > col.bounds.min.y && mousePosition.y < col.bounds.max.y))
        {
            return true;
        }
        return false;
    }

}
