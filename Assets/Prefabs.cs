using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    public GameObject textMesh;
    public GameObject myPrefab3D;


    private List<GameObject> instantiatedObjects = new List<GameObject>();
    private List<GameObject> instantiatedObjectsText = new List<GameObject>();

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {

    }

    public void MakePrefab(string label, float depth, float position_x, float position_y, float height, float width,Vector3 loc,Quaternion rot,Matrix4x4 matrix){
        GameObject clone;
        GameObject text;
        
        // Apply extrinsics to the relative position of the bounding box.
        Vector3 vector3 = matrix*(new Vector3(position_x, (-1)*position_y, 4));
        // Create bounding box in the position and rotation
        clone = Instantiate(myPrefab, loc + 
            vector3, rot);
        // adjust the size of the box
        clone.transform.localScale = new Vector3((float)(width),(float)(height),(float)(0.1));

        // add text in the same position with the name of the class
        text = Instantiate(textMesh, loc + vector3, rot);
        if(depth == 0.0){
            text.GetComponent<TextMesh>().text = label;
        }else{
            text.GetComponent<TextMesh>().text = label + " - " + string.Format("{0:N2}", depth) + "m";
        }

        // Keep a list of holograms that are added, so they can be later removed. 
        instantiatedObjects.Add(clone);
        instantiatedObjectsText.Add(text);
    }

    // Attempt at creating 3D bounding boxes
    public void MakePrefab3D(string label,float position_x, float position_y, float height, float width,Vector3 loc,Quaternion rot,Matrix4x4 matrix, float depth){
        GameObject clone;
        GameObject text;
        
        // Instantiate at position (0, 0, 0) and zero rotation.
        // Vector3 vector3 = matrix*(new Vector3((x1+x2)/2, (-1)*(y1+y2+(float)(0.3))/2, 2));
        Vector3 vector3 = matrix*(new Vector3(position_x, (-1)*position_y - height*depth/10, depth));
        clone = Instantiate(myPrefab3D, loc + 
            vector3, rot);
        clone.transform.localScale = new Vector3((float)(width)*depth/4,(float)(width)*depth/4,(float)(width)*depth/4);
        clone.transform.Rotate((float)(120),(float)(180),(float)(0));
        text = Instantiate(textMesh, loc + vector3, rot);
        text.transform.localScale = new Vector3((float)(0.1)*depth/4,(float)(0.1)*depth/4,(float)(0.1)*depth/4);
        text.GetComponent<TextMesh>().text = label + " - " + string.Format("{0:N2}", depth) + "m";
        instantiatedObjects.Add(clone);
        instantiatedObjectsText.Add(text);
        // Debug.Log(Camera.main.transform.position);
        // Debug.Log(Camera.main.transform.rotation);
    }

    public void ClearPrefabs()
    {
        foreach(var go in instantiatedObjects) {
            GameObject.Destroy(go);
        }
        instantiatedObjects.Clear();
        foreach(var text in instantiatedObjectsText) {
            GameObject.Destroy(text);
        }
        instantiatedObjectsText.Clear();
    }
}
