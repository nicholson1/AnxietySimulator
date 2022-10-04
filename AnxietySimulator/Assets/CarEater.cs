using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEater : MonoBehaviour
{
    
    public static event Action<Car> EatCar;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            EatCar(other.transform.parent.parent.GetComponent<Car>());
        }
    }
}
