using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawning : MonoBehaviour
{

    int numAnimalsEach = 4;
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
        SpawnAnimals();
        StartCoroutine(audio());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAnimals()
    {
        for(int i = 0; i < numAnimalsEach; i++)
        {

            animalInstances.Add(Instantiate(monkey, new Vector3(Random.Range(-45.0f, 45.0f), 5.0f, Random.Range(-35.0f, 35.0f)), Quaternion.identity), false);
            animalInstances.Add(Instantiate(gecko, new Vector3(Random.Range(-45.0f, 45.0f), 5.0f, Random.Range(-35.0f, 35.0f)), Quaternion.identity), false);
            animalInstances.Add(Instantiate(muskrat, new Vector3(Random.Range(-45.0f, 45.0f), 5.0f, Random.Range(-35.0f, 35.0f)), Quaternion.identity), false);
            animalInstances.Add(Instantiate(deer, new Vector3(Random.Range(-45.0f, 45.0f), 5.0f, Random.Range(-35.0f, 35.0f)), Quaternion.identity), false);
            animalInstances.Add(Instantiate(bird, new Vector3(Random.Range(-45.0f, 45.0f), 5.0f, Random.Range(-35.0f, 35.0f)), Quaternion.identity), false);
            animalInstances.Add(Instantiate(snake, new Vector3(Random.Range(-45.0f, 45.0f), 5.0f, Random.Range(-35.0f, 35.0f)), Quaternion.identity), false);
            //animalInstances.Add(Instantiate(snake, new Vector3(0, 5.0f, 0), Quaternion.identity), false);
        }
    }
    IEnumerator audio()
    {
        while (true)
        {
            foreach (KeyValuePair<GameObject, bool> animal in animalInstances)
            {
                // do something with entry.Value or entry.Key
                if (animal.Value == false)
                {
                    animal.Key.GetComponent<AudioSource>().Play();
                }
            }
            yield return new WaitForSeconds(5);
        }
    }
}
