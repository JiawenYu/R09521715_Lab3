using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnerEntity : MonoBehaviour
{

    float m_Velocity = 0;
    public float OWNER_ACCELERATION = 0.1f;
    public float OWNER_DECELERATION = 0.1f;
    public float OWNER_MIN_VELOCITY = 0f;
    public float OWNER_MAX_VELOCITY = 0.5f;

    void Start()
    {

    }

    [SerializeField] SpriteRenderer[] m_Renderders = new SpriteRenderer[1];

    void ResetColor()
    {
        ChangeColor(Color.white);
    }
    void ChangeColor(Color color)
    {
        foreach (SpriteRenderer r in m_Renderders)
        {
            r.color = color;
        }
    }
    void Stop()
    {
        m_Velocity = 0;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Stop();
        print(collision.gameObject.name);
        ChangeColor(Color.red);
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        Stop();
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        ResetColor();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckPoint checkPoint = collision.gameObject.GetComponent<CheckPoint>();
        if (checkPoint != null)
        {
            ChangeColor(Color.green);
            this.Invoke("ResetColor", 0.1f);
        }
    }

    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Speed up
            m_Velocity = Mathf.Min(m_Velocity + OWNER_ACCELERATION * Time.fixedDeltaTime, OWNER_MAX_VELOCITY);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Speed down
            m_Velocity = Mathf.Min(m_Velocity - OWNER_ACCELERATION * Time.fixedDeltaTime, OWNER_MAX_VELOCITY);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Turn left
            //transform.Rotate(new Vector3(0, 0, 10));
            gameObject.transform.Rotate(0, 0, 30 * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Turn left
            //transform.Rotate(new Vector3(0, 0, -10));
            gameObject.transform.Rotate(0, 0, -30 * Time.fixedDeltaTime);
        }
        this.transform.Translate(m_Velocity * Time.fixedDeltaTime * Vector2.right);
    }
    
}
