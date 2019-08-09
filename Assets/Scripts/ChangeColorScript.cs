using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorScript : MonoBehaviour
{

    public Color color = Color.red;
    Renderer[] rend;
    MaterialPropertyBlock block;
    private void Start()
    {
        rend = GetComponentsInChildren<Renderer>();
        block = new MaterialPropertyBlock();
        SetColor(color);
    }




    void SetColor(Color color)
    {
        block.SetColor("_BaseColor", color);
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].SetPropertyBlock(block);

        }
    }
}
