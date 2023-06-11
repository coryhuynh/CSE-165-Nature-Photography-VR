using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectorRay : MonoBehaviour
{
    public LineRenderer line;
    LineRenderer actual;
    RaycastHit currhit;
    public CamScript camScript;
    // Start is called before the first frame update
    void Start()
    {
        actual = Instantiate(line);
        actual.enabled = true;
        Vector3[] positions = new Vector3[2];
        positions[0] = new Vector3(-2.0f, -2.0f, 0.0f);
        positions[1] = new Vector3(0.0f, 2.0f, 0.0f);
        actual.SetPositions(positions);
    }

    // Update is called once per frame
    void Update()
    {
        actual.SetPosition(0, transform.position);
        actual.SetPosition(1, transform.TransformDirection(Vector3.forward) * 500);
        if (!camScript.camEnabled)
        {
            regularRay();
        }
        else
        {
            actual.enabled = false;
        }
    }


    void regularRay()
    {
        actual.enabled = true;
        RaycastHit hit;
        int layers = 127;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layers))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            actual.SetPosition(1, transform.position + (transform.TransformDirection(Vector3.forward) * hit.distance));

            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown(KeyCode.R))
            {
                if(hit.collider.gameObject.layer == 5)
                {
                    Button btn = hit.collider.gameObject.GetComponent<Button>();
                    if(btn != null)
                    {
                        btn.onClick.Invoke();
                    }
                }
            }
        }


    }
}
