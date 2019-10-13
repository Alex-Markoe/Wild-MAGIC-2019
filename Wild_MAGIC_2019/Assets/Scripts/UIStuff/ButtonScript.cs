using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public int indexOfTransition;

    //public bool quitGame;

    // Start is called before the first frame update
    void Start()
    {
        //indexOfTransition = SceneManager.GetActiveScene().buildIndex;
        //if(indexOfTransition > SceneManager.sceneCount - 1)
        //{
        //    indexOfTransition = 0;
        //}        
        //else if(quitGame)
        //{
        //    indexOfTransition = -1;
        //}
        //else
        //{
        //    indexOfTransition++;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
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
}
