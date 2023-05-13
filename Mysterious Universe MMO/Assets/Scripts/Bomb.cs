using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int damage = 10;
    public float speed;
    public GameObject[] multipleEnemies;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, speed * Time.deltaTime);
        if (transform.position.y == 0)
        {
            BombDestoy();
        }
        
    }

    public void BombDestoy()
    {
        //Tìm những enemy có tag là enemy
        multipleEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        for(int i=0; i <= multipleEnemies.Length-1; i++)
        {
            LifeController life = multipleEnemies[i].GetComponent<LifeController>();
            if (life != null)//nếu vật va chạm có script LifeController
            {
                life.TakeDamage(damage);//Gây damage
                
            }
        }
        anim.SetTrigger("Exploision");
        Destroy(gameObject,0.4f);//Hủy vật này

    }
}
