using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private float radius = 1.6f;

    public void SetKnife(Vector3 pos)
    {
        float angle = Random.Range(0, 360);
        transform.localPosition = pos + (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)) * radius; // 자리 잡고

        Vector3 dir = transform.localPosition - pos;
        float angleZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, angleZ - 90f); // 돌리기
    }
}
