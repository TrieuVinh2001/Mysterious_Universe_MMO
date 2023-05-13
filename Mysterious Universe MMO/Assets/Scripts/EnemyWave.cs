using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public Transform[] shotSpawns;//Các điểm bắn
    public GameObject enemyBullet;//Đạn

    [HideInInspector]
    public int shotChance; //probability of 'Enemy's' shooting during tha path
    [HideInInspector]
    public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path

    // Start is called before the first frame update
    void Start()
    {

        //Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));
        InvokeRepeating("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax), Random.Range(shotTimeMin, shotTimeMax));
    }

    void ActivateShooting()
    {
        if (Random.value < (float)shotChance / 100)                             //if random value less than shot probability, making a shot
        {
            //Instantiate(enemyBullet, gameObject.transform.position, Quaternion.identity);
            for (int i = 0; i < shotSpawns.Length; i++)
            {
                Instantiate(enemyBullet, shotSpawns[i].position, shotSpawns[i].rotation);
            }
        }
    }
}
