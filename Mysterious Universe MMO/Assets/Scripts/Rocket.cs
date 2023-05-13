using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;
    public Transform closestEnemy;
    public GameObject[] multipleEnemies;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        closestEnemy = GetClosestEnemy();

        if(closestEnemy != null)
        {
            ChasingEnemy();
        }

    }

    public void ChasingEnemy()
    {
        Vector2 lookDirection = (closestEnemy.transform.position - transform.position);
        transform.up = new Vector2(lookDirection.x, lookDirection.y);

        transform.position = Vector2.MoveTowards(transform.position, closestEnemy.position, speed * Time.deltaTime);
    }

    public Transform GetClosestEnemy()
    {
        //Tìm những enemy có tag là enemy
        multipleEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Lấy giá trị gần nhất
        float closestDistance = Mathf.Infinity;
        Transform enemyPos = null;//Cho vị trí enemy là null

        //Tìm qua list các kẻ thù để tìm kẻ thù gần nhất và đặt làm mục tiêu
        foreach(GameObject enemies in multipleEnemies)
        {
            //Lấy khoảng cách với từng kẻ thù 1
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, enemies.transform.position);

            //Nếu khoảng cách hiện tại < khoảng cách gần nhất
            if (currentDistance < closestDistance)
            {
                //Khoảng cách gần nhất trở thành khoảng cách hiện tại
                closestDistance = currentDistance;
                //Lấy vị trí của kẻ thù
                enemyPos = enemies.transform;
            }
        }
        return enemyPos;
    }
}
