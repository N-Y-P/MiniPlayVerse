using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int obstacleCount = 0; // 장애물의 개수
    public Vector3 obstacleLastPosition = Vector3.zero; // 마지막으로 배치된 장애물의 위치
    public int numBgCount = 4;//처음에 만들때 다섯개씩이었어서

    void Start()
    {
        // 씬에 존재하는 모든 Obstacle 객체를 배열로 가져오기
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        // 첫 번째 장애물의 위치를 obstacleLastPosition에 저장
        obstacleLastPosition = obstacles[0].transform.position;
        // 장애물의 개수를 계산하여 저장
        obstacleCount = obstacles.Length;

        // 장애물 개수만큼 반복하여 각 장애물의 위치를 랜덤하게 설정
        for (int i = 0; i < obstacleCount; i++)
        {
            // SetRandomPlace 함수는 각 장애물의 위치를 이전 장애물 위치를 기반으로 설정함
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Background"))//뒷배경도 반복
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;

            return;
        }
        // 충돌한 객체가 Obstacle인지 확인
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            // 장애물이 충돌 시 랜덤 위치로 재배치
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }

    }
}
