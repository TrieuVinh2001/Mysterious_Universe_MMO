﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBulletP : MonoBehaviour
{
    private GameObject target;
    public float speed;
    private Rigidbody2D bulletRB;


    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;//Hướng player
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);//Bắn theo hướng có player
    }

}
