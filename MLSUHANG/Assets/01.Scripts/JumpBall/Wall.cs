using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private SpriteRenderer visual;

    private int colorID;
    public int ColorID => colorID;

    private void Start()
    {
        visual = transform.Find("Visual").GetComponent<SpriteRenderer>();
    }
    public void SetWall(Color c, int id)
    {
        visual.color = c;
        colorID = id;
    }
}
