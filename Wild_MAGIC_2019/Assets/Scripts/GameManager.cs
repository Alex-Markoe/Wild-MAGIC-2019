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
    public float cutsceneTimer;
    public float cutsceneMax = 3f;
    public bool cutsceneActive;
    public bool cutsceneEndActive;
    public bool cutsceneFirst;

    private int sceneIndex;
    private static CardType playerChosen;

    private AudioSource source;

    public AudioClip bossSong;
    public AudioClip regularSong;
    // Start is called before the first frame update
    void Start()
    {
        first = true;
        Object.DontDestroyOnLoad(this);
        source = GetComponent<AudioSource>();
        cutsceneActive = true;
        cutsceneEndActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            source.Play();
        }
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
                cutsceneActive = true;
                first = true;
                cutsceneFirst = true;
                SceneManager.LoadScene(2);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            //source.Stop();
            source.clip = bossSong;
            if (first)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            }
        }
        else
        {
            //source.Stop();
            source.clip = regularSong;
        }
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            if(first)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
                player.SetLightRadius(currentLight);
            }
            
        }
        if(SceneManager.GetActiveScene().buildIndex == 32 || SceneManager.GetActiveScene().buildIndex == 5)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (player.hp <= 0)
            {
                first = true;
                SceneManager.LoadScene(7);
            }
        }
    }

    public void Win()
    {
        SceneManager.LoadScene(6);
    }

    public void FinalDoor()
    {
        SceneManager.LoadScene(4);
    }

    public void StartCutscene()
    {
        SceneManager.LoadScene(3);
    }
    public void EndCutscene()
    {
        first = true;
        SceneManager.LoadScene(5);
    }
}
