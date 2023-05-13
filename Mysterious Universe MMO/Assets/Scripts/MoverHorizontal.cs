using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverHorizontal : MonoBehaviour
{

    public float speed;
    private Vector2 point;
    private bool moveRight;

    void Start()
    {
        point= GetComponentInParent<SpawnHorizontal>().point;
        //StartCoroutine(moveSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, point, speed * Time.deltaTime);
        //if(gameObject.transform.position.x == point.x)
        //{
           
        //        transform.position = Vector2.MoveTowards(transform.position, point * p, speed * Time.deltaTime);
            
            
        //}
        
    }

    //IEnumerator moveSpawn()
    //{
    //    yield return new WaitForSeconds(2f);

    //    moveRight = !moveRight;
    //}
}
