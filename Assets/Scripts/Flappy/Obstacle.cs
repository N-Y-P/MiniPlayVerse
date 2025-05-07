using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;// 장애물이 배치될 수 있는 Y축 상한선
    public float lowPosY = 1f;

    //탑과 바텀 사이의 공간을 얼마나 가져갈것인지
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;// 각 장애물 간의 X축 간격

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;
        topObject.localPosition = new Vector3(0, halfHoleSize);//홀사이즈의 반만큼 위로 올리기
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlaneController player = other.GetComponent<PlaneController>();
        if (player != null)
        {
            ScoreManager.Instance.AddScore(1);
        }
    }

}
