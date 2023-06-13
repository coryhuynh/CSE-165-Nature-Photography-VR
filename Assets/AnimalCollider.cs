using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public HashSet<GameObject> animals;
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
            RaycastHit hit;
            Vector3 heading = collision.gameObject.transform.position - player.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            Debug.DrawRay(player.transform.position, direction * distance, Color.yellow);
            if (Physics.Raycast(player.transform.position, direction, out hit, Mathf.Infinity))
            {
                if(hit.distance >= distance - 9.0f && hit.distance <= distance + 9.0f)
                {
                    animals.Add(collision.gameObject);
                    Debug.Log(collision.gameObject.name);
                    return;
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
