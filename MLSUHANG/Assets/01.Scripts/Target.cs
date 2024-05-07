using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [Header("====================")]
    [Header("Target")]
    [SerializeField] private float speed = 80f;
    private Vector3 angle;

    [Header("====================")]
    [Header("KNIFE")]
    [SerializeField] private Transform knifeTrm;
    [SerializeField] private Knife knifePref;
    private List<Knife> knifeList;

    /*[Header("Knife UI")]
    [SerializeField] private Transform knifeUIParent;
    [SerializeField] private Image[] knifeUIs;
    [SerializeField] private Image knifeUIPref;*/

    //private int knifeCnt;
    //private int currentKnifeCnt = 0;
    //private List<KnifeAgent> knifeList;

    [Header("====================")]
    [Header("APPLE")]
    [SerializeField] private Transform appleTrm;
    [SerializeField] private Apple applePref;
    private Apple currentApple;
    public Apple CurrentApple => currentApple;

    private void Start()
    {
        StartCoroutine(ChangeSpeed());
    }

    private void Update()
    {
        transform.Rotate(speed * angle * Time.deltaTime);
    }

    private IEnumerator ChangeSpeed()
    {
        while (true)
        {
            float time = Random.Range(3f, 10f);
            angle = Random.Range(0, 2) == 0 ? Vector3.forward : Vector3.back;
            yield return new WaitForSeconds(time);

            speed = Random.Range(50f, 80f);
            yield return null;
        }
    }

    public void SetGame()
    {
        knifeList = new List<Knife>();

        int knifeCnt = Random.Range(1, 6);

        Apple newApple = Instantiate(applePref, appleTrm);
        newApple.SetApple(appleTrm.localPosition);
        currentApple = newApple;

        for (int i = 0; i < knifeCnt; ++i)
        {
            Knife newKnife = Instantiate(knifePref, knifeTrm);
            newKnife.SetKnife(knifeTrm.localPosition);
            knifeList.Add(newKnife);
        }

        //knifeUIs = knifeUIParent.GetComponentsInChildren<Image>();
    }

    /*public void ShootKnife(KnifeAgent k)
    {
        currentKnifeCnt = Mathf.Clamp(++currentKnifeCnt, 0, knifeCnt);
        knifeUIs[currentKnifeCnt - 1].color = Color.black;
        knifeList.Add(k); // 꽂힌 나이프 리스트에 넣어주기
    }*/

    public void ResetGame()
    {
        Destroy(currentApple.gameObject);
        foreach(var k in knifeList)
        {
            Destroy(k.gameObject);
        }

        knifeList.Clear();
    }
}
