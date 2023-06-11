using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelRay : MonoBehaviour
{
    public LineRenderer line;
    public LineRenderer tline;
    

    List<Vector3> pointList = new List<Vector3>();
    LineRenderer travelLine;
    LineRenderer actual;
    RaycastHit currhit;

    bool traveling;
    int currTravel;
    bool moving;
    int currPoint;
    public GameObject cam;
    float t;

    public HandMenuScript handMenuScript;
    public TimerScoreScript timerScoreScript;

    // Start is called before the first frame update
    void Start()
    {
        t=0;
        currTravel=0;
        moving=false;
        actual = Instantiate(line);
        actual.enabled = true;
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
        actual.SetPosition(0, transform.position);
        actual.SetPosition(1, transform.TransformDirection(Vector3.forward) * 500);
        if(moving){
            int layers =127;
            float dur = 1f/70f;
            t+=Time.deltaTime;
            
            while(t>=dur){
                t-=dur;
                if(pointList.Count==0){
                    moving=false;
                    t=0;
                    actual.enabled=true;
                    break;
                }
                cam.transform.position=travelLine.GetPosition(0)+ new Vector3(0,1.399f,0);
                
                pointList.RemoveAt(0);
                travelLine.positionCount=pointList.Count;
                travelLine.SetPositions(pointList.ToArray());

            }


        }
        else{
            if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.K)){
                traveling=false;
                t=0;
                moving=true;
                currTravel=0;
                actual.enabled=false;
            }
            if(!traveling){
                if(handMenuScript.menuEnabled || !timerScoreScript.gameStart){
                    actual.enabled = false;
                }
                else{
                    regularRay();
                }
            }
            else{
                travelingRay();
            }
        }
        
    }
    void regularRay(){
        actual.enabled = true;
        RaycastHit hit;
        int layers =127;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,layers))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                actual.SetPosition(1, transform.position + (transform.TransformDirection(Vector3.forward) * hit.distance));
       
                
                
                if ((OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && !handMenuScript.menuEnabled) || Input.GetKeyDown(KeyCode.L))
                 {
                    float distance=Mathf.Sqrt(Mathf.Pow(hit.point.x-cam.transform.position.x,2)+Mathf.Pow(hit.point.z-cam.transform.position.z,2));
                    if(hit.collider.gameObject.layer==6&&distance<10){
                        currPoint=0;
                        travelLine=Instantiate(tline);
                        travelLine.positionCount=2;
                        travelLine.SetPosition(0,hit.point);
                        pointList.Add(hit.point);
                        travelLine.SetPosition(++currPoint,hit.point);
                        pointList.Add(hit.point);
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
                    pointList.Add(hit.point);
                }
                
                    
            
            }
        }
    }

}
