using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingCameraEntity : MonoBehaviour
{
    //public GameObject targetObject;
    //void Update()
    //{
    //    Vector3 deltaPos = targetObject.transform.position - this.transform.position;
    //    Vector3 position = deltaPos * 0.9f * Time.deltaTime;

    //    this.transform.position += new Vector3(position.x, position.y, 0);
    //}

    public CarEntity targetObject;
    Camera m_Camera;
    float m_OrthographicSize;
    public float Moving_Threshold = 3f;

    void Start() 
    {
        m_Camera = this.GetComponent<Camera>();
        m_OrthographicSize = m_Camera.orthographicSize;
    }
    void LateUpdate()
    {
        Vector2 deltaPos = this.transform.position - targetObject.transform.position;
        m_Camera.orthographicSize = m_OrthographicSize + targetObject.m_Velocity * 0.2f;
        if (deltaPos.magnitude > Moving_Threshold)
        {
            deltaPos.Normalize ();

            Vector2 newPos = new Vector2(targetObject.transform.position.x,
                targetObject.transform.position.y) + deltaPos * Moving_Threshold;
            
            this.transform.position += new Vector3(newPos.x, newPos.y, this.transform.position.z);
        }
    }
}
