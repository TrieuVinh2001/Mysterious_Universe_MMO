using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodging : MonoBehaviour
{
    public float speed;//Tốc độ di chuyển
    public Boundary boundary;//Vùng giới hạn di chuyển
    private Rigidbody2D rb;
    private float target;//Vị trí
    public float dodgeValue;//giá trị dịch chuyển
    //Dùng vector thay cho việc khai báo 2 biến min max
    public Vector2 startWait;//thời gian chờ khi bắt đầu
    public Vector2 dodgeTime;//thời gian di chuyển
    public Vector2 dodgeWait;//Thời gian chờ để né

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Dodge());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dodgingbehaviour = Mathf.MoveTowards(rb.velocity.x, target, speed);//biến di chuyển vị trí trục x cần đến
        rb.velocity = new Vector2(dodgingbehaviour, rb.velocity.y);//di chuyển đến vị trí cần đến

        rb.position = new Vector2(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax)); //Giới hạn vị trí di chuyển
    }

    IEnumerator Dodge()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));//thực hiện sau khoảng thời gian được random
        while (true)//chưa rõ tác dụng
        {
            target = Random.Range(1, dodgeValue) * -Mathf.Sign(transform.position.x);//vị trí được lấy bởi giá trị random * -vị trí của nó hiện tại 
            yield return new WaitForSeconds(Random.Range(dodgeTime.x, dodgeTime.y));//Sau 1 khoảng thời gian random (đây sẽ là thời gian di chuyển từ vị trí trước đó)
            target = 0;//Dừng lại
            yield return new WaitForSeconds(Random.Range(dodgeWait.x, dodgeWait.y));//Thời gian chờ để thực hiện lần né(tức di chuyển của enemy) tiếp theo
        }
        
    }
}
