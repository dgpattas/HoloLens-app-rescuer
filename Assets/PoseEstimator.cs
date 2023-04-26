using UnityEngine;
using System.Collections;
using UnityEngine.Windows.WebCam;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit;
using UnityEngine.Networking;
using System;
using System.Linq;
// using HoloLensCameraStream;

#if !UNITY_EDITOR && UNITY_WSA_10_0
using System;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
#endif


public class PoseEstimator : MonoBehaviour
{
    Prefabs prefab;

    public GameObject debugger;
    public string url;

    public async void startroutine(){
        
        StartCoroutine(GetBoundingBox());
    }

    
    IEnumerator GetBoundingBox(){

        // loc is the translation of the HoloLens in the Unity space
        // rot is the rotation
        // matrix is for conerting coordinates relative to the HoloLens to Global coordinates

        // Read all of these "closer" to the time that the images was captured to minimize misplacement due to sudden movements. 
        // Unfortunately we cannot know the position of the HoloLens in the exact time that each image is captured. 
        Vector3 loc = Camera.main.transform.position;
        Quaternion rot = Camera.main.transform.rotation;
        Matrix4x4 matrix = Camera.main.transform.localToWorldMatrix;
        string uri = url + "/bbox";
        
        // request json from tool's internal server
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
            yield return req.SendWebRequest();
            if(req.result == UnityWebRequest.Result.Success){
                string returnedValues = req.downloadHandler.text;
                Debug.Log(returnedValues);

                // Root Object is a list of detections
                RootObject myObject;
                myObject = JsonUtility.FromJson<RootObject>(returnedValues);

                // Because we do not have the intrinscs of the gopro camera, and we do not have the exact position of the camera relative to the hololens.
                // We added positionWeight, scaleWeight with empirical values to approximate the correct placement of the boxes.
                // The placements are accurate in distances >1.5 meters.
                // Intrinsics of the GoPro and extrinsics between camera and hololens could be measured, maybe yielding better results
                // Increasing positionWeight moves the center of the bounding box closer to the center of the fov of the hololens
                // Increasing the scaleWeight decreases the size of the bounding box
                if(myObject.toolData.Count != 0){
                    prefab.ClearPrefabs();
                    for(int i=0; i<myObject.toolData.Count; i++){
                        int positionWeight = 110;
                        int scaleWeight = 500;
                        float posx,posy,height,width;
                        posx=(myObject.toolData[i].relative_x_center-(640/2))/positionWeight;
                        posy=(myObject.toolData[i].relative_y_center- (480/2))/positionWeight;
                        height = myObject.toolData[i].box_height/scaleWeight;
                        width = myObject.toolData[i].box_width/scaleWeight;
                        prefab.MakePrefab(myObject.toolData[i].label_Id, myObject.toolData[i].depth, posx, posy, height, width, loc, rot, matrix);
                    }
                }  
            }
        }
    }
    void Start()
    {
        // Debugg is a class for displaying messages 
        Debugg debugg = debugger.GetComponent<Debugg>();
        prefab = GameObject.FindGameObjectWithTag("prefab").GetComponent<Prefabs>();

        // We have added a "url.txt" file on the app's folder "Windows.Storage.ApplicationData.Current.LocalFolder"
        // This url.txt file is useful in case the address that the Robust Vision tool is running from changes, e.g. for debugging purposes on different laptop than the one running the tool. 
        // url.txt contains the url of the Robust Vision's internal server with which this app communicates in order to retrieve the json messages from the broker. 
        // We did not find a soft way of configuring this url so it is hard-coded on a file which is easily accessible and editable from the HoloLens Portal.
        read_file(debugg);

        // Wait for the file to be read. 
        StartCoroutine(wait());

        // Start reading the json from the broker and show bbox.
        InvokeRepeating("startroutine", 0f, 0.3f);
    }

    IEnumerator wait(){
        yield return new WaitForSeconds(4.0f);
    }

    async void read_file(Debugg debugg){
    #if !UNITY_EDITOR
            Debug.Log("trying to read host from configuration file");
                    Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    Windows.Storage.StorageFile configuration_file;
                    try
                    {
                        configuration_file = await storageFolder.GetFileAsync("url.txt");
                        url = await Windows.Storage.FileIO.ReadTextAsync(configuration_file);
                        Debug.Log("text from configuration file is :: " + url);
                    }
                    catch(System.IO.FileNotFoundException e)
                    {
                        Debug.Log("configuration file not found! set DNS value : faster.inov.pt");
                        url = "http://195.251.8.1:5500";
                    }
    #endif
    }

}

[System.Serializable]
public class DetectionInfo
{
    public string label_Id;
    public float relative_x_center,relative_y_center,box_width,box_height,depth;
    public float confidence;

    
    // Given JSON input:
    // {"name":"Dr Charles","lives":3,"health":0.8}
    // this example will return a PlayerInfo object with
    // name == "Dr Charles", lives == 3, and health == 0.8f.
}

[Serializable]
public class RootObject
{
    public List<DetectionInfo> toolData;
}

