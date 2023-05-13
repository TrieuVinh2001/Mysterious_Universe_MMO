using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodamage : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)//Kiểm tra va chạm
    {
        //Lấy script LifeController của vật va chạm
        LifeController life = other.GetComponent<LifeController>();

        if(life != null)//nếu vật va chạm có script LifeController
        {
            life.TakeDamage(damage);//Gây damage
            Destroy(gameObject);//Hủy vật này
        }
    }
}
