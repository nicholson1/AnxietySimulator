using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CarPrefab;
    [SerializeField] private Transform[] ListOfpaths;
    [SerializeField] private Transform CarPooler;
    [SerializeField] private float CarSpawnRate;
    [SerializeField] private Transform SpawnLocation;
    
    //private List<>

    List<Car> DeadList = new List<Car>();

    private float count = 0;


    private void Start()
    {
        SpawnCar();
        CarEater.EatCar += EatCar;
    }
    
    private void OnDestroy()
    {
        CarEater.EatCar -= EatCar;
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count > CarSpawnRate)
        {
            SpawnCar();
            count = 0;
        }
    }

    public void SpawnCar()
    {
        if (DeadList.Count != 0)
        {
            //Debug.Log(DeadList.Count);
            DeadList[0].gameObject.transform.position = SpawnLocation.position;
            DeadList[0].gameObject.SetActive(true);
            DeadList[0].Initialize();
            DeadList.RemoveAt(0);
        }
        else
        {
            
            Car spawn = Instantiate(CarPrefab, CarPooler).GetComponent<Car>();
            spawn.transform.position = SpawnLocation.position;
            spawn.Path = ListOfpaths[Random.Range(0,ListOfpaths.Length)];
            spawn.Initialize();

        }
        
    }

    private void EatCar(Car car)
    {
        DeadList.Add(car);
        car.gameObject.SetActive(false);
        
    }

   
}
