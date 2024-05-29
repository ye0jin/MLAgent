using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Apple : MonoBehaviour
{
    private float radius = 1.9f;

    public void SetApple(Vector3 pos)
    {
        float angle = Random.Range(0, 360);
        transform.localPosition = pos + (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)) * radius; // �ڸ� ���

        Vector3 dir = transform.localPosition - pos;
        float angleZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, angleZ - 90f); // ������
    }
}
