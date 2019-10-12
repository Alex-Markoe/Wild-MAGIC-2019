using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public int indexOfTransition;

    // Start is called before the first frame update
    void Start()
    {
        indexOfTransition = SceneManager.GetActiveScene().buildIndex;
        if(indexOfTransition == 2)
        {
            indexOfTransition = 0;
        }
        else
        {
            indexOfTransition++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(indexOfTransition == -1)
        {
            Application.Quit();
        }
        SceneManager.LoadScene(indexOfTransition);
    }
}
