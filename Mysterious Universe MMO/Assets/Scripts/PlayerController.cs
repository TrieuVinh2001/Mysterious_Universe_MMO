using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public enum ItemEffect
{
    levelup, shield, bomb, rocket
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Rigidbody2D rb;//vật lý chuyển động
    private SpriteRenderer sprite;
    private Animator anim;

    public int level;//Cấp độ máy bay
    public int maxlevel;//cấp độ tối đa
    public int upgradeCost;//Tiền nâng cấp
    public float speed;//tốc độ di chuyển
    public int bombAmount;//Số lượng Bom
    public GameObject bullet;//Đạn
    public GameObject shield;//Khiên
    public GameObject bomb;//Bomb
    public GameObject rocket;//Ten lua
    public float fireRate;//Thời gian nghỉ bắn
    public float fireRateRocket=5;
    public Transform[] firepoints;//Các điểm bắn
    public GameObject[] rocketPoint;
    public int lives = 1;//Số mạng sống
    public bool isDead = false;
    private Vector3 startPosition;//Điểm bắt đầu, hồi sinh
    public float spawnTime;//Thời gian hồi sinh
    public float invincibilityTime;//Thời gian bất tử

    private float nextFire=0.5f;
    private float nextFireRocket = 0.5f;
    public int fireLevel=1;//Level máy bay
    public int rocketCount=0;
    
    public Boundary boundary;//class chứa các giá trị giới hạn
    public Camera mainCamera;

    private float deltaX, deltaY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.SetLiveText(lives);//Thay đổi số mạng trên UI
        GameManager.instance.SetBombText(bombAmount);
        GameManager.instance.SetUpgradeCostText(upgradeCost);
        //Di chuyển
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;

        //Di chuyển theo vị trí tay chạm màn hình
        if (Input.touchCount == 1) // if there is a touch
        {
            Touch touch = Input.touches[0];
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);  //calculating touch position in the world space
            touchPosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, touchPosition, 20 * Time.deltaTime);
        }

        //Di chuyển theo khoảng cách tay di chuyển khi ấn
        //if (Input.touchCount > 1)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    Vector2 touchPos = mainCamera.ScreenToWorldPoint(touch.position);

        //    switch (touch.phase)
        //    {
        //        case TouchPhase.Began:
        //            deltaX = touchPos.x - transform.position.x;
        //            deltaY = touchPos.y - transform.position.y;
        //            break;

        //        case TouchPhase.Moved:
        //            rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
        //            break;

        //        case TouchPhase.Ended:
        //            rb.velocity = Vector2.zero;
        //            break;
        //    }
        //}

        //Giới hạn vị trí
        rb.position = new Vector2(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax));
        if (!isDead)
        {
            //Bắn đạn nếu nhấn chuột và thời gian thỏa mãn
            if (Time.time > nextFire)
            {
                AudioManager.instance.PlaySFX(10);
                nextFire = Time.time + fireRate;
                //Các cấp độ ứng với số đạn
                if (fireLevel == 1)
                {
                    GameObject bullet0 = Instantiate(bullet, firepoints[0].position, firepoints[0].rotation);
                    PlayerBullet b0 = bullet0.GetComponent<PlayerBullet>();
                    b0.dierction = (firepoints[0].localRotation * Vector2.up).normalized;//Tạo đạn ở vị trí điểm thứ nhất
                }
                if (fireLevel == 2)
                {
                    GameObject bullet1 = Instantiate(bullet, firepoints[1].position, firepoints[1].rotation);
                    PlayerBullet b1 = bullet1.GetComponent<PlayerBullet>();
                    b1.dierction = (firepoints[1].localRotation * Vector2.up).normalized;

                    GameObject bullet2 = Instantiate(bullet, firepoints[2].position, firepoints[2].rotation);
                    PlayerBullet b2 = bullet2.GetComponent<PlayerBullet>();
                    b2.dierction = (firepoints[2].localRotation * Vector2.up).normalized;
                }
                if (fireLevel == 3)
                {
                    GameObject bullet0 = Instantiate(bullet, firepoints[0].position, firepoints[0].rotation);
                    PlayerBullet b0 = bullet0.GetComponent<PlayerBullet>();
                    b0.dierction = (firepoints[0].localRotation * Vector2.up).normalized;

                    GameObject bullet1 = Instantiate(bullet, firepoints[1].position, firepoints[1].rotation);
                    PlayerBullet b1 = bullet1.GetComponent<PlayerBullet>();
                    b1.dierction = (firepoints[1].localRotation * Vector2.up).normalized;

                    GameObject bullet2 = Instantiate(bullet, firepoints[2].position, firepoints[2].rotation);
                    PlayerBullet b2 = bullet2.GetComponent<PlayerBullet>();
                    b2.dierction = (firepoints[2].localRotation * Vector2.up).normalized;
                }
                if (fireLevel == 4)
                {

                    GameObject bullet1 = Instantiate(bullet, firepoints[1].position, firepoints[1].rotation);
                    PlayerBullet b1 = bullet1.GetComponent<PlayerBullet>();
                    b1.dierction = (firepoints[1].localRotation * Vector2.up).normalized;

                    GameObject bullet2 = Instantiate(bullet, firepoints[2].position, firepoints[2].rotation);
                    PlayerBullet b2 = bullet2.GetComponent<PlayerBullet>();
                    b2.dierction = (firepoints[2].localRotation * Vector2.up).normalized;

                    GameObject bullet3 = Instantiate(bullet, firepoints[3].position, firepoints[3].rotation);
                    PlayerBullet b3 = bullet3.GetComponent<PlayerBullet>();
                    b3.dierction = (firepoints[3].localRotation * Vector2.up).normalized;

                    GameObject bullet4 = Instantiate(bullet, firepoints[4].position, firepoints[4].rotation);
                    PlayerBullet b4 = bullet4.GetComponent<PlayerBullet>();
                    b4.dierction = (firepoints[4].localRotation * Vector2.up).normalized;
                }
                if (fireLevel == 5)
                {
                    GameObject bullet0 = Instantiate(bullet, firepoints[0].position, firepoints[0].rotation);
                    PlayerBullet b0 = bullet0.GetComponent<PlayerBullet>();
                    b0.dierction = (firepoints[0].localRotation * Vector2.up).normalized;

                    GameObject bullet1 = Instantiate(bullet, firepoints[1].position, firepoints[1].rotation);
                    PlayerBullet b1 = bullet1.GetComponent<PlayerBullet>();
                    b1.dierction = (firepoints[1].localRotation * Vector2.up).normalized;

                    GameObject bullet2 = Instantiate(bullet, firepoints[2].position, firepoints[2].rotation);
                    PlayerBullet b2 = bullet2.GetComponent<PlayerBullet>();
                    b2.dierction = (firepoints[2].localRotation * Vector2.up).normalized;

                    GameObject bullet3 = Instantiate(bullet, firepoints[3].position, firepoints[3].rotation);
                    PlayerBullet b3 = bullet3.GetComponent<PlayerBullet>();
                    b3.dierction = (firepoints[3].localRotation * Vector2.up).normalized;

                    GameObject bullet4 = Instantiate(bullet, firepoints[4].position, firepoints[4].rotation);
                    PlayerBullet b4 = bullet4.GetComponent<PlayerBullet>();
                    b4.dierction = (firepoints[4].localRotation* Vector2.up).normalized;
                }

                if (fireLevel == 6)
                {
                    GameObject bullet1 = Instantiate(bullet, firepoints[1].position, firepoints[1].rotation);
                    PlayerBullet b1 = bullet1.GetComponent<PlayerBullet>();
                    b1.dierction = (firepoints[1].localRotation * Vector2.up).normalized;

                    GameObject bullet2 = Instantiate(bullet, firepoints[2].position, firepoints[2].rotation);
                    PlayerBullet b2 = bullet2.GetComponent<PlayerBullet>();
                    b2.dierction = (firepoints[2].localRotation * Vector2.up).normalized;

                    GameObject bullet3 = Instantiate(bullet, firepoints[3].position, firepoints[3].rotation);
                    PlayerBullet b3 = bullet3.GetComponent<PlayerBullet>();
                    b3.dierction = (firepoints[3].localRotation * Vector2.up).normalized;

                    GameObject bullet4 = Instantiate(bullet, firepoints[4].position, firepoints[4].rotation);
                    PlayerBullet b4 = bullet4.GetComponent<PlayerBullet>();
                    b4.dierction = (firepoints[4].localRotation * Vector2.up).normalized;

                    GameObject bullet5 = Instantiate(bullet, firepoints[5].position, firepoints[5].rotation);
                    PlayerBullet b5 = bullet5.GetComponent<PlayerBullet>();
                    b5.dierction = (firepoints[5].localRotation * Vector2.up).normalized;

                    GameObject bullet6 = Instantiate(bullet, firepoints[6].position, firepoints[6].rotation);
                    PlayerBullet b6 = bullet6.GetComponent<PlayerBullet>();
                    b6.dierction = (firepoints[6].localRotation * Vector2.up).normalized;
                }
                if (fireLevel == 7)
                {
                    GameObject bullet0 = Instantiate(bullet, firepoints[0].position, firepoints[0].rotation);
                    PlayerBullet b0 = bullet0.GetComponent<PlayerBullet>();
                    b0.dierction = (firepoints[0].localRotation * Vector2.up).normalized;

                    GameObject bullet1 = Instantiate(bullet, firepoints[1].position, firepoints[1].rotation);
                    PlayerBullet b1 = bullet1.GetComponent<PlayerBullet>();
                    b1.dierction = (firepoints[1].localRotation * Vector2.up).normalized;

                    GameObject bullet2 = Instantiate(bullet, firepoints[2].position, firepoints[2].rotation);
                    PlayerBullet b2 = bullet2.GetComponent<PlayerBullet>();
                    b2.dierction = (firepoints[2].localRotation * Vector2.up).normalized;

                    GameObject bullet3 = Instantiate(bullet, firepoints[3].position, firepoints[3].rotation);
                    PlayerBullet b3 = bullet3.GetComponent<PlayerBullet>();
                    b3.dierction = (firepoints[3].localRotation * Vector2.up).normalized;

                    GameObject bullet4 = Instantiate(bullet, firepoints[4].position, firepoints[4].rotation);
                    PlayerBullet b4 = bullet4.GetComponent<PlayerBullet>();
                    b4.dierction = (firepoints[4].localRotation * Vector2.up).normalized;

                    GameObject bullet5 = Instantiate(bullet, firepoints[5].position, firepoints[5].rotation);
                    PlayerBullet b5 = bullet5.GetComponent<PlayerBullet>();
                    b5.dierction = (firepoints[5].localRotation * Vector2.up).normalized;

                    GameObject bullet6 = Instantiate(bullet, firepoints[6].position, firepoints[6].rotation);
                    PlayerBullet b6 = bullet6.GetComponent<PlayerBullet>();
                    b6.dierction = (firepoints[6].localRotation * Vector2.up).normalized;
                }
            }

            //if(Input.GetMouseButtonDown(1)&& rocketAmount>0)
            //{
            //    Instantiate(rocket, firepoints[0].position, firepoints[0].rotation);
            //    rocketAmount--;
            //    AudioManager.instance.PlaySFX(8);
            //}
        }
        BulletRocket();
    }

    private void BulletRocket()
    {
        if(!isDead)
        {
            if (Time.time > nextFireRocket)
            {
                nextFireRocket = Time.time + fireRateRocket;
                if (rocketCount == 1)
                {
                    rocketPoint[0].SetActive(true);
                    Instantiate(rocket, rocketPoint[0].transform.position, rocketPoint[0].transform.rotation);
                    AudioManager.instance.PlaySFX(8);
                }
                if (rocketCount == 2)
                {
                    rocketPoint[0].SetActive(true);
                    rocketPoint[1].SetActive(true);
                    Instantiate(rocket, rocketPoint[0].transform.position, rocketPoint[0].transform.rotation);
                    Instantiate(rocket, rocketPoint[1].transform.position, rocketPoint[1].transform.rotation);
                    AudioManager.instance.PlaySFX(8);

                }
            }
        }
    }


    public void Bomb()//Gán vào nút bắn rocket
    {
        if (bombAmount > 0)
        {
            Instantiate(bomb, firepoints[0].position, Quaternion.identity);//Tạo rocket
            bombAmount--;
            AudioManager.instance.PlaySFX(8);
        }

    }

    public void Respawn()//Hồi sinh
    {
        lives--;//Trừ mạng
        if (lives > 0)//Nếu còn mạng
        {
            StartCoroutine(Spawning());//Spawn
        }
        else
        {
            lives = 0;
            isDead = true;
            sprite.enabled = false;//Ấn hình ảnh
            gameObject.layer = 10;//Đổi layer sang enemy để k bị dính damage
        }
    }

    IEnumerator Spawning()
    {
        isDead = true;
        sprite.enabled = false;
        //fireLevel = 0;//Để k bắn được
        fireLevel -= 1;//level đạn reset về 1
        if (fireLevel <= 1)
        {
            fireLevel = 1;
        }
        gameObject.layer = 10;//Đổi layer sang enemy để k bị dính damage
        yield return new WaitForSeconds(spawnTime);//Thời gian đợi hồi sinh
        isDead = false;
        transform.position = startPosition;//xét vị trí hồi sinh
        for(float i=0; i< invincibilityTime; i += 0.1f)//Dùng để hình ảnh ấn hiện liên tục
        {
            sprite.enabled = !sprite.enabled;//xét ẩn hoặc hiện
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.layer = 8;//Đổi về layer Player
        sprite.enabled = true;//Hiện ảnh
        
    }

    public void setItemEffect(ItemEffect effect)
    {
        if(effect == ItemEffect.levelup)
        {
            AudioManager.instance.PlaySFX(7);
            fireLevel++;
            if (fireLevel >= 7)
            {
                fireLevel = 7;
            }
        }


        else if (effect == ItemEffect.shield)
        {
            AudioManager.instance.PlaySFX(7);
            Instantiate(shield, transform);//Tạo khiên tại vị trí player
        }
        else if (effect == ItemEffect.bomb)
        {
            AudioManager.instance.PlaySFX(7);
            bombAmount++;//Tăng số bomb có thể bắn
        }
        else if (effect == ItemEffect.rocket)
        {
            AudioManager.instance.PlaySFX(7);
            rocketCount++;//Tăng số bomb có thể bắn
            if (rocketCount >= 2)
            {
                rocketCount = 2;
            }
        }
    }

    public void AddLevel()
    {
        if(upgradeCost <= GameManager.instance.money)//Nếu tiền nhiều hơn tiên nâng cấp
        {
            level++;//Tăng 1 cấp
            fireRate -= 0.025f;//Giảm thời gian chờ bắn
            lives++;
            GameManager.instance.money -= upgradeCost;//Trừ tiền nâng cấp
            //PlayerPrefs.SetInt("Money", GameManager.instance.money);//Lưu tiền

            upgradeCost *= 2;//Tăng số tiền cần để nâng cấp
            AudioManager.instance.PlaySFX(10);//Phát nhạc
            anim.SetTrigger("Upgrade");
        }
    }

}
