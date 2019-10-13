using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public TarotCardManager cardManager;
    public EnemyManager enemyManager;

    private int sceneIndex;
    private static CardType playerChosen;
    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(sceneIndex == 1)
        {
            playerChosen = CardType.None;
        }
        if(sceneIndex == 2)
        {
            Debug.Log(playerChosen);
            if (playerChosen == CardType.Sun)
            {
                //Change the lights radius
                for (int i = 0; i < enemyManager.enemies.Length; i++)
                {
                    enemyManager.enemies[i].movementSpeed *= 5f;
                }
            }
            else if(playerChosen == CardType.Death)
            {
                player.hp = 1;
                player.attackDamage *= 2;
            }
            else if(playerChosen == CardType.Moon)
            {
                //Will Change players radius to shrink and implement dash attack soon
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(sceneIndex == 1)
        {
            playerChosen = cardManager.CardChosen();
            if (playerChosen != CardType.None)
            {
                for (int i = 0; i < 3; i++)
                {
                    Destroy(cardManager.cardList[0].GetComponent<SpriteRenderer>());
                    Destroy(cardManager.cardList[0]);
                    cardManager.cardList.RemoveAt(0);
                }
                SceneManager.LoadScene(2);
            }
        }
        if(sceneIndex == 2)
        {
            if (player.hp <= 0)
            {
                SceneManager.LoadScene(4);
            }
            /*if(Win condition)
             * {
             * SceneManager.LoadScene(3);
             * }
             * */
        }
    }
}
