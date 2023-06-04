using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelRay : MonoBehaviour
{
    public LineRenderer line;
    public LineRenderer tline;
    LineRenderer travelLine;
    LineRenderer actual;
    RaycastHit currhit;
    bool traveling;
    int currPoint;
    public GameObject cam;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        t=0;
        actual = Instantiate(line);
        currPoint=0;
        Vector3[] positions = new Vector3[2];
        positions[0] = new Vector3(-2.0f, -2.0f, 0.0f);
        positions[1] = new Vector3(0.0f, 2.0f, 0.0f);
        actual.SetPositions(positions);
        traveling = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        actual.enabled = true;
        actual.SetPosition(0, transform.position);
        actual.SetPosition(1, transform.TransformDirection(Vector3.forward) * 500);
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) || Input.GetKeyDown(KeyCode.L)){
            traveling=false;
            currPoint=0;
            Destroy(travelLine);
        }
        if(!traveling){
            regularRay();
        }
        else{
            travelingRay();
        }
        
    }
    void regularRay(){
        RaycastHit hit;
        int layers =127;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,layers))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                actual.SetPosition(1, transform.position + (transform.TransformDirection(Vector3.forward) * hit.distance));
       
                
                
                if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || Input.GetKeyDown(KeyCode.L))
                 {
                    float distance=Mathf.Sqrt(Mathf.Pow(hit.point.x-cam.transform.position.x,2)+Mathf.Pow(hit.point.z-cam.transform.position.z,2));
                    if(hit.collider.gameObject.layer==6&&distance<10){
                        travelLine=Instantiate(tline);
                        travelLine.positionCount=2;
                        travelLine.SetPosition(0,hit.point);
                        travelLine.SetPosition(++currPoint,hit.point);
                        currPoint++;
                        Debug.Log(hit.point);
                        
                        traveling=true;
                    }
                 }
            }
            
        
    }
    void travelingRay(){
        RaycastHit hit;
        int layers =127;
        float dur = 1f/90f;
        t+=Time.deltaTime;
        while(t>=dur){
            t-=dur;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,layers))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                actual.SetPosition(1, transform.position + (transform.TransformDirection(Vector3.forward) * hit.distance));
                
                
                
                if(hit.collider.gameObject.layer==6){
                    travelLine.positionCount=currPoint+1;
                    travelLine.SetPosition(currPoint++,hit.point);
                }
                
                    
            
            }
        }
    }

}
