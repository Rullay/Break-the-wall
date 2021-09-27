using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject gameController;
    [SerializeField] private GameObject StartMenu;
    [SerializeField] private GameObject tapToStart;
    [SerializeField] private GameObject again;
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private Text numberLevel;

    void Start()
    {
        StartMenu.SetActive(true);
    }



    void Update()
    {
        
    }

    public void StartGame()
    {
        gameController.GetComponent<GameController>().StartGame();
        tapToStart.SetActive(false);
    }

    public void ÑreateLevel()
    {
        StartMenu.SetActive(false);
        gameController.GetComponent<GameController>().ÑreateLevel();
        tapToStart.SetActive(true);
    }

    public void Again()
    {
        if(again.activeSelf == false)
        {
            again.SetActive(true);
        }
        else
        {
            gameController.GetComponent<GameController>().Again();
            again.SetActive(false);
            tapToStart.SetActive(true);
        }

    }

    public void NextLevel()
    {
        if (nextLevel.activeSelf == false)
        {
            nextLevel.SetActive(true);
        }
        else
        {
            gameController.GetComponent<GameController>().NextLevel();
            nextLevel.SetActive(false);
            tapToStart.SetActive(true);
        }
    }

    public void NumberLevel(int number)
    {
        numberLevel.text = "" + number;
    }
        

    public void NewLevel()
    {
        gameController.GetComponent<GameController>().RandomLevel();
    }

    

    
}
