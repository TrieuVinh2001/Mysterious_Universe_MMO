using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public Vector2 dierction = new Vector2(1, 0);


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Di chuyển theo hướng lên
        //rb.velocity = Vector2.up * speed;
        rb.velocity = dierction * speed;
        Vector2 pos = transform.position;
        pos += rb.velocity * Time.deltaTime;
        transform.position = pos;
    }
}
