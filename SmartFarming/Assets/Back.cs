using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour {

	public void BackButton()
    {
        SceneManager.LoadScene(1);
    }
    public void SensorInfo()
    {
        SceneManager.LoadScene(2);
    }
    public void Realtime()
    {
        SceneManager.LoadScene(3);
    }
}
