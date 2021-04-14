using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSensor : MonoBehaviour
{
    public CarEntity carscript;
    bool righten;

    public float sensor_d = 10;
    public Color sensorcolor;
    public Vector3 sensordir;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, sensordir, sensor_d);

        if (hit == true)
        {
            
            if (hit.collider.tag == "edge")
            {
                //righten = false;
                Debug.Log("hit");
                carscript.Stop();
                carscript.ChangeColor(sensorcolor);
            }
            
        }
    }
}
