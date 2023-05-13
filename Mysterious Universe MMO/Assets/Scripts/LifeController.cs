using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public int health;//Máu
    public int moneyToGive;//Tiền của enemy
    private int maxhealth;//Dùng cho player thôi
    public GameObject explosion;//Hiệu ứng nổ
    public Color damageColor;//Màu ảnh khi dính damage
    private Color firt;
    public bool isDead = false;
    private SpriteRenderer sprite;

    public GameObject[] dropItem;//Item
    private static int chanceToDropItem = 0;//tỉ lệ rơi item

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        maxhealth = health;
        firt = sprite.color;
    }


    public void TakeDamage(int damage)//Hàm gây damage
    {
        if (!isDead)
        {
            health -= damage;//Trừ máu

            if (health <= 0)//Nếu hết máu
            {
                AudioManager.instance.PlaySFX(2);
                if (explosion != null)
                {
                    Instantiate(explosion, transform.position, transform.rotation);//Tạo ra hiệu ứng nổ tại điểm xác định
                }
                
                if(this.GetComponent<PlayerController>() != null)//Kiểm tra nếu là Player
                {
                    GetComponent<PlayerController>().Respawn();//Trừ 1 mạng
                    health = maxhealth;//Đầy máu sau khi hồi sinh
                }
                else
                {
                    chanceToDropItem++;//Tăng tỉ lệ khi phá hủy enemy
                    int random = Random.Range(0, 100);//Dùng để so sánh với tỉ lệ
                    if(random<chanceToDropItem&& dropItem.Length > 0)
                    {
                        //Tạo item tại vị trí enemy bị phá hủy
                        Instantiate(dropItem[Random.Range(0, dropItem.Length)],transform.position,Quaternion.identity);
                        chanceToDropItem = 0;//reset tỉ lệ về 0
                    }

                    isDead = true;
                    GameManager.instance.money += moneyToGive;//Tăng tiền khi giết đc enemy
                    //PlayerPrefs.SetInt("Money", GameManager.instance.money);
                    Destroy(gameObject);//Phá hủy gameObject
                }
            }
            else
            {
                StartCoroutine(TakingDamage());//Sẽ đổi màu
            }
        }
    }

    IEnumerator TakingDamage()
    {
        sprite.color = damageColor;//Đổi màu ảnh

        yield return new WaitForSeconds(0.1f);
        sprite.color = firt;//Màu trở về ban đầu
    }
}
