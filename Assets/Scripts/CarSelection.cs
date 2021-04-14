using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelection : MonoBehaviour
{
    public GameObject[] Cars;

    private int currentCarIndex;
    void Start()
    {
        currentCarIndex = 0;
        Cars = GameObject.FindGameObjectsWithTag("cars");

        Debug.Log("Number of cars is: ," + Cars.Length + ",");
        //Turn all cameras off, except the first default one
        for (int i = 1; i < Cars.Length; i++)
        {
            Cars[i].gameObject.SetActive(false);
        }
        //If any cameras were added to the controller, enable the first one
        if (Cars.Length > 0)
        {
            Cars[0].gameObject.SetActive(true);
            Debug.Log("Car with name: " + Cars[0].name + ", is now enabled");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentCarIndex++;
            Debug.Log("C button has been pressed. Switching to the next car");

            if (currentCarIndex < Cars.Length)
            {
                Cars[currentCarIndex - 1].gameObject.SetActive(false);
                Cars[currentCarIndex].gameObject.SetActive(true);
                Debug.Log("Car with name: " + Cars[currentCarIndex].name + ", is now enabled");
            }
            else
            {
                Cars[currentCarIndex - 1].gameObject.SetActive(false);
                currentCarIndex = 0;
                Cars[currentCarIndex].gameObject.SetActive(true);
                Debug.Log("Car with name: " + Cars[currentCarIndex].name + ", is now enabled");
            }
        }
    }
}
