using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class JumpBallManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private Transform scoreVisual;
    private TextMeshPro scoreText;
    private int score = 0;
    public int Score => score;

    [Header("Transform")]
    [SerializeField] private Transform leftTrm;
    [SerializeField] private Transform rightTrm;

    public Transform target;
    private int targetLayer = 15;

    [Header("Lists")]
    [SerializeField] private Color[] colors;
    public Color[] Colors => colors;
    [SerializeField] private int[] colorID;
    public int[] ColorID => colorID;

    private void Start()
    {
        scoreText = scoreVisual.GetComponentInChildren<TextMeshPro>();
    }

    public void SetGame()
    {
        SetWalls(leftTrm);
        SetWalls(rightTrm);
        ResetScore();
    }

    public void SetWalls(Transform t)
    {
        List<int> randList = new List<int>();
        Wall[] walls = t.GetComponentsInChildren<Wall>();

        float x = t.localPosition.x < 0 ? -1 : 1;

        for (int i = 0; i < walls.Length; i++)
        {
            randList.Add(i);
        }

        for (int i = 0; i < walls.Length; i++)
        {   
            int idx = Random.Range(0, randList.Count);
            int randIdx = randList[idx];
            walls[i].SetWall(colors[randIdx], colorID[randIdx]);

            randList.RemoveAt(idx);
        }

        t.DOLocalMoveX(x * 3.25f, 0.2f).OnComplete(() => t.DOLocalMoveX(x * 3.0f, 0.3f));
    }

    public void SetTargetTransform(int id, float x)
    {
        foreach (Transform t in x < 0 ? leftTrm : rightTrm)
        {
            if(t.TryGetComponent<Wall>(out Wall w))
            {
                if(w.ColorID == id)
                {
                    if(target != null) target.gameObject.layer = 0;
                    target = t;
                    target.gameObject.layer = targetLayer;
                }
            }
        }
    }

    public void CheckWall(float x)
    {
        SetWalls(x > 0 ? leftTrm : rightTrm);
    }

    public void AddScore()
    {
        scoreVisual.DOScale(4.3f, 0.3f).OnComplete(() => scoreVisual.DOScale(4f, 0.2f));
        scoreText.text = $"{++score}";
    }
    public void ResetScore()
    {
        score = 0;
        scoreText.text = $"{score}";
    }
}
