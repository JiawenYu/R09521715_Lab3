using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public float ThetaScale = 0.01f;
    public float radius = 3f;
    private int Size;
    private LineRenderer circledraw;
    private float Theta = 360f;

    void Start()
    {
        circledraw = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        circledraw.positionCount = Size;
        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radius * Mathf.Cos(Theta);
            float y = radius * Mathf.Sin(Theta);
            circledraw.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
