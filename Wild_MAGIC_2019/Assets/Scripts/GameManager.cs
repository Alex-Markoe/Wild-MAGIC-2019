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
    }

    // Update is called once per frame
    void Update()
    {
        if(!source.isPlaying)
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
                SceneManager.LoadScene(7);
            }
        }
        if(SceneManager.GetActiveScene().buildIndex == 7)
        {
            if(first)
            {
                cutsceneTimer = cutsceneMax;
                first = false;
            }
            if (cutsceneTimer > 0)
            {
                cutsceneTimer -= Time.deltaTime;
            }
            else if (cutsceneActive)
            {
                first = true;
                cutsceneActive = false;
                StartCutscene();
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            if (first)
            {
                cutsceneTimer = cutsceneMax;
                first = false;
            }
            if (cutsceneTimer > 0)
            {
                cutsceneTimer -= Time.deltaTime;
            }
            else if (cutsceneActive)
            {
                cutsceneActive = false;
                first = true;
               EndCutscene();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            //source.Stop();
            source.clip = bossSong;
        }
        else
        {
            //source.Stop();
            source.clip = regularSong;
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
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
        if(SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (player.hp <= 0)
            {
                first = true;
                SceneManager.LoadScene(5);
            }
        }
    }

    public void Win()
    {
        SceneManager.LoadScene(4);
    }

    public void FinalDoor()
    {
        cutsceneActive = true;
        SceneManager.LoadScene(8);
    }

    public void StartCutscene()
    {
        first = true;
        SceneManager.LoadScene(2);
    }
    public void EndCutscene()
    {
        SceneManager.LoadScene(3);
    }
}
