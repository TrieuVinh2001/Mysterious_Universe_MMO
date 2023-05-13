using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Shooting
{
    [Range(0, 100)]
    [Tooltip("probability with which the ship of this wave will make a shot")]
    public int shotChance;//Tỉ lệ bắn

    [Tooltip("min and max time from the beginning of the path when the enemy can make a shot")]
    public float shotTimeMin, shotTimeMax;//Thời gian bắn min max để random
}
public class Wave : MonoBehaviour
{
    #region FIELDS
    [Tooltip("Enemy's prefab")]
    public GameObject enemy;//Kẻ thù

    [Tooltip("a number of enemies in the wave")]
    public int count;//Số lượng kẻ thù

    [Tooltip("path passage speed")]
    public float speed;//Tốc độ

    public float waitCreat;

    [Tooltip("time between emerging of the enemies in the wave")]
    public float timeBetween;//Thời gian giữa mỗi kẻ thù xuất hiện

    [Tooltip("points of the path. delete or add elements to the list if you want to change the number of the points")]
    public Transform[] pathPoints;//Các điểm di chuyển

    [Tooltip("whether 'Enemy' rotates in path passage direction")]
    public bool rotationByPath;//kẻ thù xoay theo hướng đường đi k

    [Tooltip("if loop is activated, after completing the path 'Enemy' will return to the starting point")]
    public bool Loop;//Nếu true thì kr thì quay lại điểm đầu

    [Tooltip("color of the path in the Editor")]
    public Color pathColor = Color.yellow;//Màu đường đi
    public Shooting shooting;//Đạn

    //[Tooltip("if testMode is marked the wave will be re-generated after 3 sec")]
    //public bool testMode;//Nếu true thì làn sóng được tạo lại sau 3 giây
    #endregion

    private void Start()
    {
        StartCoroutine(CreateEnemyWave());
    }

    //Tạo kẻ thù và xác định các tham số
    IEnumerator CreateEnemyWave() //depending on chosed parameters generating enemies and defining their parameters
    {
        yield return new WaitForSeconds(waitCreat);
        for (int i = 0; i < count; i++) //Duyệt từng kẻ thù
        {
            GameObject newEnemy;
            newEnemy = Instantiate(enemy, enemy.transform.position, Quaternion.identity);//Đặt biến tạo ra kẻ thù
            FollowThePath followComponent = newEnemy.GetComponent<FollowThePath>();//gọi script FollowThePath
            followComponent.path = pathPoints;         //Cho path của kẻ thù bằng các điểm ở đây
            followComponent.speed = speed;
            followComponent.rotationByPath = rotationByPath;
            followComponent.loop = Loop;
            followComponent.SetPath();

            EnemyWave enemyComponent = newEnemy.GetComponent<EnemyWave>();
            enemyComponent.shotChance = shooting.shotChance;
            enemyComponent.shotTimeMin = shooting.shotTimeMin;
            enemyComponent.shotTimeMax = shooting.shotTimeMax;
            newEnemy.SetActive(true);      //Tạo kẻ thù
            yield return new WaitForSeconds(timeBetween); //Chờ thời gian tạo kẻ thù tiếp theo
        }
        /*if (testMode)       //if testMode is activated, waiting for 3 sec and re-generating the wave
        {
            yield return new WaitForSeconds(3);
            StartCoroutine(CreateEnemyWave());
        }
        else*/
        if (!Loop)
            Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        DrawPath(pathPoints);
    }

    void DrawPath(Transform[] path) //drawing the path in the Editor
    {
        Vector3[] pathPositions = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            pathPositions[i] = path[i].position;
        }
        Vector3[] newPathPositions = CreatePoints(pathPositions);
        Vector3 previosPositions = Interpolate(newPathPositions, 0);
        Gizmos.color = pathColor;
        int SmoothAmount = path.Length * 20;
        for (int i = 1; i <= SmoothAmount; i++)
        {
            float t = (float)i / SmoothAmount;
            Vector3 currentPositions = Interpolate(newPathPositions, t);
            Gizmos.DrawLine(currentPositions, previosPositions);
            previosPositions = currentPositions;
        }
    }

    Vector3 Interpolate(Vector3[] path, float t)
    {
        int numSections = path.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * numSections), numSections - 1);
        float u = t * numSections - currPt;
        Vector3 a = path[currPt];
        Vector3 b = path[currPt + 1];
        Vector3 c = path[currPt + 2];
        Vector3 d = path[currPt + 3];
        return 0.5f * ((-a + 3f * b - 3f * c + d) * (u * u * u) + (2f * a - 5f * b + 4f * c - d) * (u * u) + (-a + c) * u + 2f * b);
    }

    Vector3[] CreatePoints(Vector3[] path)  //using interpolation method calculating the path along the path points
    {
        Vector3[] pathPositions;
        Vector3[] newPathPos;
        int dist = 2;
        pathPositions = path;
        newPathPos = new Vector3[pathPositions.Length + dist];
        Array.Copy(pathPositions, 0, newPathPos, 1, pathPositions.Length);
        newPathPos[0] = newPathPos[1] + (newPathPos[1] - newPathPos[2]);
        newPathPos[newPathPos.Length - 1] = newPathPos[newPathPos.Length - 2] + (newPathPos[newPathPos.Length - 2] - newPathPos[newPathPos.Length - 3]);
        if (newPathPos[1] == newPathPos[newPathPos.Length - 2])
        {
            Vector3[] LoopSpline = new Vector3[newPathPos.Length];
            Array.Copy(newPathPos, LoopSpline, newPathPos.Length);
            LoopSpline[0] = LoopSpline[LoopSpline.Length - 3];
            LoopSpline[LoopSpline.Length - 1] = LoopSpline[2];
            newPathPos = new Vector3[LoopSpline.Length];
            Array.Copy(LoopSpline, newPathPos, LoopSpline.Length);
        }
        return (newPathPos);
    }
}
