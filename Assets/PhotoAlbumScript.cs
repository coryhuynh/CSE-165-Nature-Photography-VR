using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoAlbumScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int total;
    public GameObject frame;
    int curr;
    void Start()
    {
        this.GetComponent<RawImage>().texture=null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startAlbum(){
        string filename = "C:/Users/alobo/Downloads/"+curr+".png";
        byte[] bytes = System.IO.File.ReadAllBytes(filename);
        Texture2D loadTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        loadTexture.LoadImage(bytes);
        this.GetComponent<RawImage>().texture=loadTexture;
    }
    public void Next(){
        curr++;
        string filename = "C:/Users/alobo/Downloads/"+curr+".png";
        byte[] bytes = System.IO.File.ReadAllBytes(filename);
        Texture2D loadTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        loadTexture.LoadImage(bytes);
        this.GetComponent<RawImage>().texture=loadTexture;
    }
    public void Prev(){
        curr--;
        if(curr<0){
            curr= total-1;
        }
        string filename = "C:/Users/alobo/Downloads/"+curr+".png";
        byte[] bytes = System.IO.File.ReadAllBytes(filename);
        Texture2D loadTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        loadTexture.LoadImage(bytes);
        this.GetComponent<RawImage>().texture=loadTexture;

    }
    public void SpawnFrame(){
        GameObject newFrame = Instantiate(frame);
        newFrame.transform.position=transform.position+new Vector3(0,3,0);
        string filename = "C:/Users/alobo/Downloads/"+curr+".png";
        byte[] bytes = System.IO.File.ReadAllBytes(filename);
        Texture2D loadTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        loadTexture.LoadImage(bytes);
        newFrame.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<RawImage>().texture=loadTexture;
    }

}
