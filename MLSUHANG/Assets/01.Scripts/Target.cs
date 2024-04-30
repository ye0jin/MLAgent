using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Transform appleTrm;
    [SerializeField] private Apple applePref;
    private List<Apple> appleList;

    public void SetAppleList()
    {
        appleList = new List<Apple>();
        int cnt = Random.Range(1, 5);
        for (int i = 0; i < cnt; ++i)
        {
            float angle = Random.Range(0, 360);
            Apple newApple = Instantiate(applePref, appleTrm);

            newApple.transform.position = transform.position + (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)) * radius;

            Vector3 direction = newApple.transform.position-transform.position;
            float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            newApple.transform.rotation = Quaternion.Euler(0f, 0f, angleZ - 90f);

            appleList.Add(newApple);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetAppleList();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ResetAppleList();
        }
    }

    public void ResetAppleList()
    {
        foreach(var a in appleList)
        {
            Destroy(a.gameObject);
        }
        appleList.Clear();
    }
}
