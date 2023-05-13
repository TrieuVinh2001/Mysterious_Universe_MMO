using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject enemyBullet;//Đạn
    public float fireRate;//Thời gian nghỉ bắn
    public Transform[] shotSpawns;//Các điểm bắn
    public Vector2 timeShoot;
    

    // Start is called before the first frame update
    void Start()
    {
        //thực hiện phương thức bắn trong thời gian và lặp lại sau thời gian
        InvokeRepeating("Fire", fireRate, Random.Range(timeShoot.x,timeShoot.y));
    }

    

    private void Fire()
    {
        //dựa vào số điểm bắn để tạo ra số chỗ bắn
        for(int i =0; i < shotSpawns.Length; i++)
        {
            Instantiate(enemyBullet, shotSpawns[i].position, shotSpawns[i].rotation);
        }
    }
}
