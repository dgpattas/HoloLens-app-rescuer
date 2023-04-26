using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// This class has all the functions that unity requires in order to send each request. 
public class sendRequest : MonoBehaviour
{
    public GameObject poseEstimator;

    public void run_denoise_on(){
        StartCoroutine(Denoise_On());
    }

    public void run_denoise_off(){
        StartCoroutine(Denoise_Off());
    }

    public void run_detect_on(){
        StartCoroutine(Detect_On());
    }

    public void run_detect_off(){
        StartCoroutine(Detect_Off());
    }

    public void run_3d_on(){
        StartCoroutine(D_on());
    }

    public void run_3d_off(){
        StartCoroutine(D_Off());
    }

    public void run_clothes(){
        StartCoroutine(Clothes());
    }

    public void run_yolo(){
        StartCoroutine(Yolo());
    }

    public void run_foot(){
        StartCoroutine(foot());
    }

    public void run_dehaze(){
        StartCoroutine(dehaze());
    }

    public void run_derain(){
        StartCoroutine(derain());
    }

    public void run_lowlight(){
        StartCoroutine(lowlight());
    }

    IEnumerator Denoise_On()
    {
        Debug.Log("Denoise On");
        string uri = poseEstimator.GetComponent<PoseEstimator>().url + "/denoise_on";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
        yield return req.SendWebRequest();
        }
    }

    IEnumerator Denoise_Off()
    {
        Debug.Log("Denoise Off");
        string uri = poseEstimator.GetComponent<PoseEstimator>().url + "/denoise_off";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
        yield return req.SendWebRequest();
        }
    }

    IEnumerator Detect_On()
    {
        Debug.Log("detect On");
        string uri = poseEstimator.GetComponent<PoseEstimator>().url + "/detect_on";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
        yield return req.SendWebRequest();
        }
    }

    IEnumerator Detect_Off()
    {
        Debug.Log("detect Off");
        string uri = poseEstimator.GetComponent<PoseEstimator>().url + "/detect_off";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
        yield return req.SendWebRequest();
        }
    }

    IEnumerator D_on()
    {
        Debug.Log("3D On");
        string uri = poseEstimator.GetComponent<PoseEstimator>().url + "/3d_on";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
        yield return req.SendWebRequest();
        }
    }

    IEnumerator D_Off()
    {
        Debug.Log("3D Off");
        string uri = poseEstimator.GetComponent<PoseEstimator>().url + "/3d_off";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
        yield return req.SendWebRequest();
        }
    }

    IEnumerator Clothes()
    {
        Debug.Log("Clothes");
        string uri = poseEstimator.GetComponent<PoseEstimator>().url + "/clothes";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
        yield return req.SendWebRequest();
        }
    }

    IEnumerator Yolo()
    {
        Debug.Log("Yolo");
        string uri = poseEstimator.GetComponent<PoseEstimator>().url + "/yolo";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
        yield return req.SendWebRequest();
        }
    }

    IEnumerator foot()
    {
        Debug.Log("foot");
        string uri = poseEstimator.GetComponent<PoseEstimator>().url + "/foot";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
        yield return req.SendWebRequest();
        }
    }

        IEnumerator lowlight()
    {
        Debug.Log("lowlight");
        string uri = poseEstimator.GetComponent<PoseEstimator>().url + "/lowlight";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
        yield return req.SendWebRequest();
        }
    }

        IEnumerator dehaze()
    {
        Debug.Log("dehaze");
        string uri = poseEstimator.GetComponent<PoseEstimator>().url + "/dehaze";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
        yield return req.SendWebRequest();
        }
    }

        IEnumerator derain()
    {
        Debug.Log("derain");
        string uri = poseEstimator.GetComponent<PoseEstimator>().url + "/derain";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
        yield return req.SendWebRequest();
        }
    }
}
