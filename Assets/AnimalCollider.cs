using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
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

            Vector3 heading = collision.gameObject.position - player.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            if (Physics.Raycast(player.transform.position, direction, out hit, distance))
            {
                if(hit.collider.gameObject == collision.gameObject)
                {
                    animals.Add(collision.gameObject);
                    Debug.Log(collision.gameObject.name);
                }
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if (animals.Contains(collision.gameObject))
            {
                animals.Remove(collision.gameObject);
                Debug.Log(collision.gameObject.name);
            }
        }
    }
}
