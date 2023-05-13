using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;
    public void StartGame()
    {
        AudioManager.instance.PlaySFX(3);//Chơi nhạc SFX thứ 4
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        AudioManager.instance.PlaySFX(3);
        Application.Quit();
    }
}
