using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public static CompleteLevel instance;
    public float scoreWin;
    public GameObject panelWin;
    
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        GameWin();
    }

    private void GameWin()
    {
        if (GameManager.instance.money >= scoreWin)
        {
            
            panelWin.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
