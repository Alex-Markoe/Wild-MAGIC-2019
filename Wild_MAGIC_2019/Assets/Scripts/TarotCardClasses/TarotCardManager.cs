using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TarotCardManager : MonoBehaviour
{
    public List<TarotCard> cardList;
    public TarotCard deathCardPrefab;
    public TarotCard sunCardPrefab;
    public TarotCard moonPrefab;

    public Camera myCamera;
    private float totalCamWidth;
    private float totalCamHeight;
    // Start is called before the first frame update
    void Start()
    {
        totalCamWidth = myCamera.orthographicSize * 2f;
        totalCamHeight = totalCamWidth * myCamera.aspect;
        

        cardList = new List<TarotCard>(3);

        cardList.Add(CreateCard(CardType.Sun, new Vector3(.5f * -totalCamWidth, (1 / 6) * -totalCamHeight, 0)));
        cardList.Add(CreateCard(CardType.Death, new Vector3(0, (1 / 6) * -totalCamHeight, 0)));
        cardList.Add(CreateCard(CardType.Moon, new Vector3(.5f * totalCamWidth, (1 / 6) * -totalCamHeight, 0)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private TarotCard CreateCard(CardType type, Vector3 position)
    {
        TarotCard card = null;
        if (type == CardType.Sun)
        {
            card = Instantiate<TarotCard>(sunCardPrefab);
        }
        else if(type == CardType.Death)
        {
            card = Instantiate<TarotCard>(deathCardPrefab);
        }
        else if(type == CardType.Moon)
        {
            card = Instantiate<TarotCard>(moonPrefab);
        }
        card.position = position;
        card.transform.position = position;

        return card;
    }

    public CardType CardChosen()
    {
        foreach(TarotCard c in cardList)
        {
            if(c.chosen == true)
            {
                return c.type;
            }
        }
        return CardType.None;
    }
}
