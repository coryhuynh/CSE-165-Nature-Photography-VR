using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HandMenuScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject leftController;
    public TimerScoreScript tsScript;
    public GameObject player;
    bool inGal;
    public TMPro.TextMeshProUGUI btnText;
    Vector3 posInNature;
    public bool menuEnabled;
    // Start is called before the first frame update
    void Start()
    {
        inGal = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) ){
            menu.SetActive(true);
            menuEnabled = true;
            leftController.SetActive(false);
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) ){
            menu.SetActive(false);
            menuEnabled = false;
            leftController.SetActive(true);
        }
    }

    public void goToGallery(){
        if(!inGal){
            posInNature = player.transform.position;
            tsScript.pauseGame = true;
            player.transform.position = new Vector3(-198, 2, 180);
            btnText.text = "Go Back";
            inGal = true;
        }
        else{
            btnText.text = "Photo Gallery";
            player.transform.position = posInNature;
            tsScript.pauseGame = false;
             inGal = false;
        }
    }
}
