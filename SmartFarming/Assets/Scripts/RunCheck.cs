using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;
public class RunCheck : MonoBehaviour {
    public GameObject obj1;
    public GameObject obj2;
    private bool ImageExist(string imname)
    {
        var target = GameObject.Find(imname);
        var stat= target.GetComponent<TrackableBehaviour>().CurrentStatus;
        return stat == TrackableBehaviour.Status.TRACKED;
    }
    // Use this for initialization

    // Update is called once per frame
    void Update() {
        if (ImageExist("Image1") || ImageExist("Image2"))
        { obj1.SetActive(true); obj2.SetActive(true); }
        else
        { obj1.SetActive(false); obj2.SetActive(false); }
    }
    public void Back()
    {
        SceneManager.LoadScene(1);
    }
}
