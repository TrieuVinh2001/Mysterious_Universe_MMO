﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
