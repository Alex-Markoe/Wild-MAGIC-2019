using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player playerPrefab;
    public Player player;
    public TarotCardManager cardManager;
    public EnemyManager enemyManager;

    private int sceneIndex;
    private static CardType playerChosen;
    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(sceneIndex == 2)
        {
            player = Instantiate<Player>(playerPrefab);
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        if(sceneIndex == 2)
        {
            if (player.hp <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
