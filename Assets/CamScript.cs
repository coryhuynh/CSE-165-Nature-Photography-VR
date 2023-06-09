using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public AnimalSpawning animalInstances;
    public AnimalCollider animalInPhoto;
    public GameObject rightController;
    public GameObject player;
    public GameObject dslrCamera;
    public Camera cam;
    public GameObject pyramid;
    bool takeShot = false;
    public bool camEnabled;
    public int score;
    float forward;
    public int total;
    public List<byte[]> pictures = new List<byte[]>();
    public Texture2D test;

    public TimerScoreScript timerScoreScript;
    // Start is called before the first frame update
    void Start()
    {
        forward = 0.0f;
        total=0;
        camEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float zoom = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;
        float oldFoc= cam.focalLength;
        cam.focalLength += zoom;
        cam.focalLength = Mathf.Clamp(cam.focalLength, 10.0f, 120.0f);
        pyramid.transform.localScale=new Vector3(pyramid.transform.localScale.x,pyramid.transform.localScale.y,pyramid.transform.localScale.z*cam.focalLength/oldFoc);
        pyramid.transform.localPosition= new Vector3(pyramid.transform.localPosition.x,pyramid.transform.localPosition.y,pyramid.transform.localPosition.z*cam.focalLength/oldFoc);

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) && timerScoreScript.gameStart && !timerScoreScript.gameOver || Input.GetKeyDown(KeyCode.M))
        {
            dslrCamera.SetActive(true);
            camEnabled = true;
            rightController.SetActive(false);
        }

        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || Input.GetKeyUp(KeyCode.M))
        {
            dslrCamera.SetActive(false);
            camEnabled = false;
            rightController.SetActive(true);
        }

        takeShot = OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown(KeyCode.Space);

        if (takeShot && camEnabled)
        {
            int mult = 1;
            bool gotPoints= false;
            foreach(GameObject animal in animalInPhoto.animals){
                
                if(!animalInstances.animalInstances[animal]){
                    gotPoints = true;
                    score += 10 * mult;
                    mult++;
                    animalInstances.animalInstances[animal] = true;
                }
            }
            if(gotPoints){
                player.GetComponent<AudioSource>().Play();
            }
            dslrCamera.GetComponent<AudioSource>().Play();
            Texture2D screenShot = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
            RenderTexture.active = cam.targetTexture;
            screenShot.ReadPixels(new Rect(0, 0, 1920, 1080), 0, 0);
            byte[] bytes = screenShot.EncodeToPNG();
            /*
            string filename = "C:/Users/alobo/Downloads/"+total.ToString()+".png";
            */
            pictures.Add(bytes);
            total++;
            //System.IO.File.WriteAllBytes(filename, bytes);
           // Debug.Log("Saved to: " + filename);
            takeShot = false;
        }
    }
}
