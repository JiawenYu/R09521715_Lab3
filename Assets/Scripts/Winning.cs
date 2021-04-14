using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winning : MonoBehaviour
{
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count = 0;
        if (count == 3)
        {
            Debug.Log("You made it!");
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
