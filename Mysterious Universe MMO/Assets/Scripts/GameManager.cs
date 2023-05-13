using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    public static GameManager instance;
    public Text liveText;
    public Text moneyText;
    public Text rocketText;
    public Text bombText;
    public Text upgradeCostText;
    public GameObject panelLose;
    public GameObject panelSetting;
    public string nameScene;
    [HideInInspector]
    public int money;//Tiền
    [HideInInspector]
    public int rocket;//Tên lửa

    //public float startWait;//Thời gian chờ lúc bắt đầu
    //public GameObject[] enemies;//Các enemy
    //public Boundary boundary;//Giới hạn
    //public Vector2 spawnWait;//Thời gian chờ tạo enemy
    //public int enemyCountMax = 10;//Số lượng enemy tối đa
    //public float spawnWaitMin;//Thời gian chờ tạo enemy nhỏ nhất
    //public float waveWait;//thời chờ gian giữa tạo làn sóng tiếp theo
    //public float waveWaitMin;//min
    [HideInInspector]
    public bool gameOver = false;
    //private int enemyCount = 1;//số enemy

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        instance = this;
        //money = PlayerPrefs.GetInt("Money");
    }

    void Start()
    {
        Time.timeScale = 1f;
        //StartCoroutine(SpawnWave());
    }

    void Update()
    {
        SetMoneyText();
        GameOver();
    }

    /*IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(startWait);
        while (!gameOver)
        {
            for(int i=0; i<enemyCount; i++)//tạo enemy
            {
                GameObject enemy = enemies[Random.Range(0, enemies.Length)];//Random loại enemy thứ bao nhiêu

                Vector3 spawnPosition = new Vector3(Random.Range(boundary.xMin, boundary.xMax), boundary.yMax, 0);//Vị trí tạo
                Instantiate(enemy, spawnPosition, Quaternion.identity);//Tạo ra enemy
                yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));//thời gian tạo giữa các enemy
            }

            enemyCount++;//Tăng số lượng enemy
            if (enemyCount >= enemyCountMax)//Nếu lớn hơn max 
            {
                enemyCount = enemyCountMax;// thì cho bằng max
                spawnWait.x -= 0.1f;//Giảm thời gian chờ tạo enemy
                spawnWait.y -= 0.1f;
                if (spawnWait.y <= spawnWaitMin)
                {
                    spawnWait.y = spawnWaitMin;
                }
                if (spawnWait.x <= spawnWaitMin)
                {
                    spawnWait.x = spawnWaitMin;
                }

                yield return new WaitForSeconds(waveWait);//Thời gian chờ giữa các làn sóng
                waveWait -= 0.1f;
                if (waveWait <= waveWaitMin)
                {
                    waveWait = waveWaitMin;
                }
            }
        }
    }*/

    public void GameOver()
    {
        if (player.lives <= 0)
        {
            gameOver = true;
            panelLose.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void GamePauseSetting()
    {
        AudioManager.instance.PlaySFX(3);
        panelSetting.SetActive(true);
        Time.timeScale= 0f;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nameScene);
    }

    public void Continue()
    {
        AudioManager.instance.PlaySFX(3);
        panelSetting.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.instance.PlaySFX(3);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        AudioManager.instance.PlaySFX(3);
    }

    public void SetLiveText(int lives)
    {
        liveText.text = "X" + lives.ToString();//Thay đổi text
    }

    public void SetMoneyText()
    {
        moneyText.text = money.ToString();//Thay đổi text
    }

    public void SetRocketText(int rocket)
    {
        rocketText.text = rocket.ToString();//Thay đổi text
    }

    public void SetBombText(int bomb)
    {
        bombText.text = bomb.ToString();//Thay đổi text
    }

    public void SetUpgradeCostText(int upgradeCost)
    {
        upgradeCostText.text = upgradeCost.ToString();//Thay đổi text
    }
}
