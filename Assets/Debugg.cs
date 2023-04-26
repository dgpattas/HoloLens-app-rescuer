using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class creates 3D text for debugging purposes.

[RequireComponent(typeof(TextMesh))]
public class Debugg : MonoBehaviour
{
    TextMesh textMesh;
    
    // Start is called before the first frame update
    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogMessage(string message)
    {
        if (textMesh.text.Length > 300)
        {
            textMesh.text = message + "\n";
        }
        else
        {
            textMesh.text += " " + message + "\n";
        }
    }
}
