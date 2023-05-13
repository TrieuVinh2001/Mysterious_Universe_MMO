using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public float startWait;//Thời gian chờ lúc bắt đầu
    public GameObject[] enemies;//Các enemy
    public Boundary boundary;//Giới hạn
    public Vector2 spawnWait;//Thời gian chờ tạo enemy
    public int enemyCountMax = 10;//Số lượng enemy tối đa
    public float spawnWaitMin;//Thời gian chờ tạo enemy nhỏ nhất
    public float waveWait;//thời chờ gian giữa tạo làn sóng tiếp theo
    public float waveWaitMin;//min
    
    private int enemyCount = 1;//số enemy

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(startWait);
        while (!GameManager.instance.gameOver)
        {
            for (int i = 0; i < enemyCount; i++)//tạo enemy
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
    }
}
