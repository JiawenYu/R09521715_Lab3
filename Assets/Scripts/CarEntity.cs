using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEntity : MonoBehaviour
{
    public GameObject wfright;
    public GameObject wfleft;
    public GameObject wbright;
    public GameObject wbleft;
    public float carLength = 1;

    //Car Steering
    float m_FrontWheelAngle = 0;
    public float Wheel_Angle_Limit = 40f;
    public float turnAngularVelocity = 20f;

    //Accelerate and Decelerate
    public float m_Velocity;
    public float acceleration = 3f;
    public float deceleration = 3f;
    public float maxVelocity = 10f;

    float m_DeltaMovement;

    float front_radius = 0;
    float back_radius = 0;

    public GameObject Owner;
    private Vector3 n_pos;
    private Quaternion n_rot;

    Vector3 m_point;
    bool parked;
    public CarSensor c_sensor;

    public int count = 0;

    public SpriteRenderer[] m_Renderers = new SpriteRenderer[1];

    //Color functions
    public void ResetColor()
    {
        ChangeColor(Color.white);
    }

    public void ChangeColor(Color color)
    {
        foreach (SpriteRenderer r in m_Renderers)
        {
            r.color = color;
        }
    }
    //Collision Responses
    public void Stop()
    {
        m_Velocity = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Stop();
        ChangeColor(Color.red);
        Debug.Log("Collided");
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Stop();
        Debug.Log("CollidedStay");
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        ResetColor();
        Debug.Log("ExitCollide");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CheckPoint checkpoint = other.gameObject.GetComponent<CheckPoint>();
        if (checkpoint != null)
        {
            ChangeColor(Color.green);
            this.Invoke("ResetColor", 0.5f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        m_point = this.transform.position;
        
        if (other.bounds.Contains(m_point))
        {
                parked = true;
                ChangeColor(Color.green);
                this.Invoke("ResetColor", 0.5f);
                Debug.Log("Parked");
        }
    }

    void GenerateOwner()
    {
        if (parked == true)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                n_pos = (this.transform.position)+ new Vector3(0, -1.5f, 0);
                n_rot = new Quaternion(0, 0, 0, 0);

                Instantiate(Owner, n_pos, n_rot);
            }
        }
    }
    

// Start is called before the first frame update
void Start()
    {
        
    }

    void Update()
    {
        GenerateOwner();
        
        
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            //accelrate
            m_Velocity = Mathf.Min(maxVelocity, 
                m_Velocity + Time.fixedDeltaTime * acceleration);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            //left
            m_FrontWheelAngle = Mathf.Clamp(
                m_FrontWheelAngle + Time.fixedDeltaTime * turnAngularVelocity, 
                -Wheel_Angle_Limit, Wheel_Angle_Limit);
            
            //UpdateWheels();
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            //right
            m_FrontWheelAngle = Mathf.Clamp(
                m_FrontWheelAngle - Time.deltaTime * turnAngularVelocity,
                -Wheel_Angle_Limit, Wheel_Angle_Limit);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            //brake
            m_Velocity = Mathf.Max(-30,
            m_Velocity - Time.fixedDeltaTime * deceleration);

            //UpdateWheels();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //brake
            Stop();
            //UpdateWheels();
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //reset steering
            m_FrontWheelAngle=0;
            //UpdateWheels();
        }

        m_DeltaMovement = m_Velocity * Time.deltaTime;

        //Update wheels by angle
        Vector3 localEulerAngles = new Vector3(0f, 0f, m_FrontWheelAngle);
        wfleft.transform.transform.localEulerAngles = localEulerAngles;
        wfright.transform.transform.localEulerAngles = localEulerAngles;

        //turning radius calculation
        front_radius = (carLength*(1/Mathf.Sin(Mathf.Deg2Rad*m_FrontWheelAngle)));
        back_radius = (carLength*(1 / Mathf.Tan(Mathf.Deg2Rad * m_FrontWheelAngle)));
        //Debug.Log(front_radius);
        //Debug.Log(back_radius);

        //Update car transform
        this.transform.Rotate(0f, 0f,
            1 / carLength * Mathf.Tan(Mathf.Deg2Rad * m_FrontWheelAngle) 
            * m_DeltaMovement * Mathf.Rad2Deg);
        
        this.transform.Translate(Vector3.up * m_DeltaMovement);
    }
}
     



