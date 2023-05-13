using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHorizontal : MonoBehaviour
{
    public static SpawnHorizontal instance;
    public float startWait;//Thời gian chờ lúc bắt đầu
    public GameObject enemy;//Các enemy
    public Transform pointSpawn;
    public Transform[] pointFinshs;
    public Boundary boundary;//Giới hạn
    public Vector2 spawnWait;//Thời gian chờ tạo enemy
    public float spawnWaitMin;//Thời gian chờ tạo enemy nhỏ nhất
    public Vector2 point;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());
        
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(startWait);
        //while (!GameManager.instance.gameOver)
        //{
            for (int i = 0; i < pointFinshs.Length; i++)//tạo enemy
            {
                point = pointFinshs[i].position;
                Vector3 spawnPosition = new Vector3(pointSpawn.position.x, pointSpawn.position.y, 0);//Vị trí tạo
                var inst =Instantiate(enemy, spawnPosition, Quaternion.identity);//Tạo ra enemy
                inst.transform.parent = gameObject.transform;
                yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));//thời gian tạo giữa các enemy
            }
        //}
    }

    
    
}