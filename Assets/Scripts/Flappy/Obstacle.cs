using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;// ��ֹ��� ��ġ�� �� �ִ� Y�� ���Ѽ�
    public float lowPosY = 1f;

    //ž�� ���� ������ ������ �󸶳� ������������
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;// �� ��ֹ� ���� X�� ����

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;
        topObject.localPosition = new Vector3(0, halfHoleSize);//Ȧ�������� �ݸ�ŭ ���� �ø���
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
