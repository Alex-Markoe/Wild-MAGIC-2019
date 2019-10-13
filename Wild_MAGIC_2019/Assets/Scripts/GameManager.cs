using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public TarotCardManager cardManager;
    public EnemyManager enemyManager;
    public float sunLight = 2.75f;
    public float deathLight = 2f;
    public float moonLight = 1.5f;
    public float currentLight;
    public bool first;

    private int sceneIndex;
    private static CardType playerChosen;
    // Start is called before the first frame update
    void Start()
    {
        first = true;
        Object.DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            if(first)
            {
                playerChosen = CardType.None;
                cardManager = GameObject.FindObjectOfType<TarotCardManager>();
            }
            playerChosen = cardManager.CardChosen();
            if (playerChosen != CardType.None)
            {
                for (int i = 0; i < 3; i++)
                {
                    Destroy(cardManager.cardList[0].GetComponent<SpriteRenderer>());
                    Destroy(cardManager.cardList[0]);
                    cardManager.cardList.RemoveAt(0);
                }
                first = true;
                SceneManager.LoadScene(2);
            }
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            if(first)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                player.SetLightRadius(currentLight);
                enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
                Debug.Log(playerChosen);
                if (playerChosen == CardType.Sun)
                {
                    currentLight = sunLight;
                    for (int i = 0; i < enemyManager.enemies.Length; i++)
                    {
                        enemyManager.enemies[i].movementSpeed *= 5f;
                    }
                }
                else if (playerChosen == CardType.Death)
                {
                    player.hp = 1;
                    player.attackDamage *= 2;
                    currentLight = deathLight;
                }
                else if (playerChosen == CardType.Moon)
                {
                    currentLight = moonLight;
                    player.dashing = true;

                }
                first = false;
            }
            if (player.hp <= 0)
            {
                first = true;
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
