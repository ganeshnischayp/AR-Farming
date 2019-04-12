using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Object2 : MonoBehaviour {
    DatabaseReference reference;
    public GameObject Main, Sensor;
    public GameObject HIText, IRText, GasText, HumidText, LDRText, TempText;
    private char num;
    // Use this for initialization
    void getVal(string key, GameObject tb)
    {
        FirebaseDatabase.DefaultInstance
  .GetReference(key).LimitToLast(1)
  .GetValueAsync().ContinueWith(task => {
      if (task.IsFaulted)
      {
          Debug.Log("Error finding " + key);  // Handle the error...
      }
      else if (task.IsCompleted)
      {
         
          var t=tb.GetComponent<Text>();
          DataSnapshot snapshot = task.Result;
          var c= snapshot.Value;
          Debug.Log("C VALUE:"+ c.ToString());
            
          t.text = t.text + c.ToString();
          // Do something with snapshot...
      }
  });
    }
    void Start()
    {
        var s=gameObject.transform.parent.name;
        num = s[s.Length - 1];
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://arfarming-b253f.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        getVal("hi_"+num, HIText);
        getVal("ir_"+num, IRText);
        getVal("gs_"+num, GasText);
        getVal("ld_" + num, LDRText);
        getVal("hu_" + num, HumidText);
        getVal("te_" + num, TempText);
        reference.ValueChanged+= HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        //Debug.Log(args.Snapshot.Key);
        // Do something with the data in args.Snapshot
        foreach (var t in args.Snapshot.Children)
        {
            if (Regex.IsMatch(t.Key, "_"+num+"$"))
            {
                var c = t.Value;
                
                if (Regex.IsMatch(t.Key, "^hi"))
                {
                    HIText.SetActive(true);
                    HIText.GetComponent<Text>().text = "Heat Index:" + c.ToString();
                }
                if (Regex.IsMatch(t.Key, "^ir"))
                {
                    IRText.SetActive(true);
                    IRText.GetComponent<Text>().text = "IR:" + c.ToString();
                }
                if (Regex.IsMatch(t.Key, "^gs"))
                {
                    GasText.SetActive(true);
                    GasText.GetComponent<Text>().text = "Gas Sensor:" + c.ToString();
                }
                if (Regex.IsMatch(t.Key, "^hu")) { 
                    HumidText.SetActive(true);
                    HumidText.GetComponent<Text>().text = "Humidity:" + c.ToString();
                }
                if (Regex.IsMatch(t.Key, "^te"))
                {
                    TempText.SetActive(true);
                    TempText.GetComponent<Text>().text = "Temperature:" + c.ToString();
                }
                if (Regex.IsMatch(t.Key, "^ld") )
                {
                    LDRText.SetActive(true);
                    LDRText.GetComponent<Text>().text = "LDR:" + c.ToString();
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ACTIVE");
    }
    public void ViewSensor(bool x)
    {
        Main.SetActive(x);
        Sensor.SetActive(!x);
    }
}