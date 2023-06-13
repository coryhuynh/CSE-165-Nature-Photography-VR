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
    GameObject frame;
    public GameObject player;
    float frameDistance;
    float maxframeDistance;
    float handDistance;
    bool scale;
    bool rotate;
    Vector3 startScale;
    float startDist;
    Quaternion relrot;
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
        actual.SetPosition(1, transform.position+ transform.TransformDirection(Vector3.forward) * 500);
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
        if(frame!=null&&OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)){
               frame =null; 
            }
        
            else if(frame!=null){
                frame.GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
                if(OVRInput.GetDown(OVRInput.RawButton.X)){
                    scale=true;
                    startDist = (GameObject.Find("LeftHandAnchor").transform.position-transform.position).magnitude;
                    if (startDist == 0)
                    {
                        startDist = 0.0001f;
                    }
                    startScale = frame.transform.localScale;
                }
                else if(OVRInput.GetDown(OVRInput.RawButton.Y)){
                    rotate=true;
                    relrot=Quaternion.Inverse(GameObject.Find("LeftHandAnchor").transform.rotation)*frame.transform.rotation;
                }
                else if(scale){
                    Scale();
                }
                else if(rotate){
                    Rotate();
                }
                else {
                frameDistance= frameDistance*Mathf.Clamp(Vector3.Distance(transform.position, player.transform.position)/(handDistance),.95f,1.03f);
                if (frameDistance>maxframeDistance){
                    frameDistance=maxframeDistance;
                }
                frame.transform.position = transform.position+transform.TransformDirection(Vector3.forward)*frameDistance;
                }
            }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
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
                else if(hit.collider.gameObject.layer==7){
                    frame = hit.collider.gameObject;
                    handDistance = Vector3.Distance(transform.position, player.transform.position);
                    frameDistance = Vector3.Distance(frame.transform.position,transform.position);
                    maxframeDistance = 1.6f*frameDistance;
                    frame.GetComponent<Rigidbody>().isKinematic = true;
                }


            }
            
            
        }


    }
    void Scale(){
        if(OVRInput.GetUp(OVRInput.RawButton.X)){
            scale=false;
        }
        else{
            float dist = (transform.position - GameObject.Find("LeftHandAnchor").transform.position).magnitude;
                if (dist == 0)
                {
                    dist = 0.0001f;
                }
                float ratio = dist / startDist;

                if (ratio < 0.2f)
                {
                    ratio = 0.25f;
                }
                if (ratio > 4f)
                {
                    ratio = 4;
                }
                frame.transform.localScale = startScale * (dist / startDist);
        }
    }
    void Rotate(){
        if(OVRInput.GetUp(OVRInput.RawButton.Y)){
            rotate=false;
        }
        else{
            frame.transform.rotation=GameObject.Find("LeftHandAnchor").transform.rotation*relrot;
        }
    }
}
