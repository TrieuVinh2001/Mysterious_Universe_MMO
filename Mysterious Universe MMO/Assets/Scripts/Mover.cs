using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Moverment
{
    metroid, follow, point, direction
}

public class Mover : MonoBehaviour
{
    private Rigidbody2D rb;
    public float minSpeed;
    public float maxSpeed;
    public Moverment mover;
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(mover == Moverment.metroid)
        {
            //Di chuyển theo hướng xuống
            rb.velocity = Vector2.down * speed;
        }     
    }

    void Update()
    {
        if (mover == Moverment.follow)
        {
            //Di chuyển theo hướng tại vị trí đối tượng
            rb.velocity = transform.up * Random.Range(minSpeed, maxSpeed);
        }
        else if (mover == Moverment.point)
        {
            //Di chuyển theo hướng xuống
            rb.velocity = Vector2.down * speed;
        }
        
    }
}
