using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Winning win;
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.Destroy(this.gameObject);
        win.count = (win.count + 1); 
    }
}
