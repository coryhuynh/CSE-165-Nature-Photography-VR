using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawning : MonoBehaviour
{

    int numAnimalsEach = 2;
    public GameObject monkey;
    public GameObject gecko;
    public GameObject muskrat;
    public GameObject deer;
    public GameObject bird;
    public GameObject snake;
    public Dictionary<GameObject, bool> animalInstances;
    // Start is called before the first frame update
    void Start()
    {
        animalInstances = new Dictionary<GameObject, bool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAnimals()
    {
        for(int i = 0; i < numAnimalsEach; i++)
        {
            animalInstances.Add(Instantiate(monkey), false);
        }
    }
}
