using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public HashSet<GameObject> animals;
    public bool picture;
    void Start()
    {
        animals = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 10)
        {
            animals.Add(collision.gameObject);
            Debug.Log(collision.gameObject.name);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 10)
        {
            animals.Remove(collision.gameObject);
            Debug.Log(collision.gameObject.name);
        }
    }
}
