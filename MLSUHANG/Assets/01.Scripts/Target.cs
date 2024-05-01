using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public static Target Instance;

    [Header("Target")]
    [SerializeField] private float speed = 80f;
    private Vector3 angle;

    [Header("KNIFE")]
    [SerializeField] private Transform knifeUIParent;
    [SerializeField] private Image[] knifeUIs;
    [SerializeField] private Image knifeUIPref;

    private int knifeCnt;
    private int currentKnifeCnt = 0;
    private List<Knife> knifeList;


    [Header("APPLE")]
    [SerializeField] private Transform appleTrm;
    [SerializeField] private Apple applePref;
    private List<Apple> appleList;

    private void Awake()
    {
        if(Instance != null) { print("TargetError"); }
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(ChangeSpeed());
    }

    private void Update()
    {
        if(currentKnifeCnt >= knifeCnt)
        {

        }

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
        appleList = new List<Apple>();
        knifeList = new List<Knife>();

        int appleCnt = Random.Range(1, 5);
        knifeCnt = appleCnt;
        currentKnifeCnt = 0;

        for (int i = 0; i < appleCnt; ++i)
        {
            Apple newApple = Instantiate(applePref, appleTrm);
            newApple.SetApple(appleTrm.position);
            appleList.Add(newApple);

            Instantiate(knifeUIPref, knifeUIParent);
        }

        knifeUIs = knifeUIParent.GetComponentsInChildren<Image>();
    }

    public void ShootKnife(Knife k)
    {
        currentKnifeCnt = Mathf.Clamp(++currentKnifeCnt, 0, knifeCnt);
        knifeUIs[currentKnifeCnt - 1].color = Color.black;
        knifeList[currentKnifeCnt] = k; // 꽂힌 나이프 리스트에 넣어주기
    }

    public void ResetGame()
    {
        foreach(var a in appleList)
        {
            Destroy(a.gameObject);
        }
        foreach(var k in knifeList)
        {
            Destroy(k.gameObject);
        }

        appleList.Clear();
        knifeList.Clear();
    }
}
