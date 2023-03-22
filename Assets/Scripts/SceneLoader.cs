using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public int indexPrev;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                SceneLoad(1);
            }
            else
            {
                PreviousSceneLoad(indexPrev);
            }
        }
    }

    public void SceneLoad(int index)
    {
        indexPrev = index;
        SceneManager.LoadScene(index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void PreviousSceneLoad(int indexPrev)
    {
        SceneManager.LoadScene(indexPrev);
    }
}
