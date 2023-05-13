using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nameScene;

    public void LoadScene()
    {
        AudioManager.instance.PlaySFX(3);//Chơi nhạc SFX thứ 4
        SceneManager.LoadScene(nameScene);
    }


}
