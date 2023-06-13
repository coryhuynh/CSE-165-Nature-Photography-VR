using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoAlbumScript : MonoBehaviour
{
    // Start is called before the first frame update
    public CamScript cam;
    public GameObject frame;
    int curr;
    int total;
    void Start()
    {
        this.GetComponent<RawImage>().texture=null;
    }

    // Update is called once per frame
    void Update()
    {
        total = cam.total;
    }
    public void startAlbum(){
        if(total==0){
            return;
        }
        //string filename = "C:/Users/alobo/Downloads/"+curr+".png";
        //byte[] bytes = System.IO.File.ReadAllBytes(filename);
        byte[] bytes = (cam.pictures[curr]);
        Texture2D loadTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        loadTexture.LoadImage(bytes);
    
        this.GetComponent<RawImage>().texture=loadTexture;
    }
    public void Next(){
        if(total==0){
            return;
        }
        curr++;
        curr = curr%total;
        
        /*tring filename = "C:/Users/alobo/Downloads/"+curr+".png";
        byte[] bytes = System.IO.File.ReadAllBytes(filename);
        Texture2D loadTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        loadTexture.LoadImage(bytes);
        this.GetComponent<RawImage>().texture=loadTexture;
        */
        byte[] bytes = (cam.pictures[curr]);
        Texture2D loadTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        loadTexture.LoadImage(bytes);
    
        this.GetComponent<RawImage>().texture=loadTexture;
    }
    public void Prev(){
        if(total==0){
            return;
        }
        curr--;
        if(curr<0){
            curr= total-1;
        }
        /*
        string filename = "C:/Users/alobo/Downloads/"+curr+".png";
        byte[] bytes = System.IO.File.ReadAllBytes(filename);
        Texture2D loadTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        loadTexture.LoadImage(bytes);
        this.GetComponent<RawImage>().texture=loadTexture;
        */
                byte[] bytes = (cam.pictures[curr]);
        Texture2D loadTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        loadTexture.LoadImage(bytes);
    
        this.GetComponent<RawImage>().texture=loadTexture;

    }
    public void SpawnFrame(){
        if(total == 0)
            return;
        GameObject newFrame = Instantiate(frame);
        newFrame.transform.position=new Vector3(-198, 2, 181);
        frame.GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
        /*
        string filename = "C:/Users/alobo/Downloads/"+curr+".png";
        byte[] bytes = System.IO.File.ReadAllBytes(filename);
        Texture2D loadTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        loadTexture.LoadImage(bytes);
        newFrame.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<RawImage>().texture=loadTexture;
        */
        byte[] bytes = (cam.pictures[curr]);
        Texture2D loadTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        loadTexture.LoadImage(bytes);
        newFrame.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<RawImage>().texture=loadTexture;
    }

}