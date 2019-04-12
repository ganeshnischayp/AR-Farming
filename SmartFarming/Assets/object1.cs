using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
public class object1 : MonoBehaviour
{
    private DateTime t1, t2, t3, t4, t5, t6;
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
          var t = tb.GetComponent<Text>();
          DataSnapshot snapshot = task.Result;
          var c = snapshot.Value;
          Debug.Log("C VALUE:" + c.ToString());

          t.text = t.text + c.ToString();
          // Do something with snapshot...
      }
  });
    }
    void Start()
    {
        var s = gameObject.transform.parent.name;
        num = s[s.Length - 1];
        Debug.Log("num="+num);
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://arfarming-b253f.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        getVal("hi_" + num, HIText);
        getVal("ir_" + num, IRText);
        getVal("gs_" + num, GasText);
        getVal("ld_" + num, LDRText);
        getVal("hu_" + num, HumidText);
        getVal("te_" + num, TempText);
        reference.ValueChanged += HandleValueChanged;
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
            if (Regex.IsMatch(t.Key, "_" + num + "$"))
            {
                var c = t.Value;
                Debug.Log(c);
                if (Regex.IsMatch(t.Key, "^hi"))
                {
                    t1 = DateTime.Now;
                    HIText.SetActive(true);
                    HIText.GetComponent<Text>().text = "Heat Index:" + c.ToString() + "\nUpdated " + t1.ToLongTimeString();
                }
                if (Regex.IsMatch(t.Key, "^ir"))
                {
                    t2 = DateTime.Now;
                    IRText.SetActive(true);
                    IRText.GetComponent<Text>().text = "IR:" + c.ToString() + "\nUpdated " + t2.ToLongTimeString();
                }
                if (Regex.IsMatch(t.Key, "^gs"))
                {
                    t3 = DateTime.Now;
                    GasText.SetActive(true);
                    var comp = GasText.GetComponent<Text>();
                    
                    comp.text = "Gas Sensor:" + c.ToString() + "\nUpdated " + t3.ToLongTimeString();
                    //comp.color = new Color(255, 0, 0);
                    
                    if (float.Parse(c.ToString()) > 500f)
                    {
                        Debug.Log("HIGH");
                        comp.color = new Color(255, 0, 0);
                    }
                    else
                    {
                        Debug.Log("LOW");
                        comp.color = new Color(0, 0, 0);
                    }
                }
                if (Regex.IsMatch(t.Key, "^hu"))
                {
                    var comp = HumidText.GetComponent<Text>();
                    if (float.Parse(c.ToString()) > 100f)
                    {
                        comp.color = new Color(255, 0, 0);
                    }
                    else
                    {
                        comp.color = new Color(0, 0, 0);
                    }
                    t4 = DateTime.Now;
                    HumidText.SetActive(true);
                    comp.text = "Humidity:" + c.ToString()+"\nUpdated " + t4.ToLongTimeString();
                }
                if (Regex.IsMatch(t.Key, "^te"))
                {
                    var comp = TempText.GetComponent<Text>();
                    if (float.Parse(c.ToString()) > 35f)
                    {
                        comp.color = new Color(255, 0, 0);
                    }
                    else
                    {
                        comp.color = new Color(0, 0, 0);
                    }
                    t5 = DateTime.Now;
                    TempText.SetActive(true);
                    comp.text = "Temperature:" + c.ToString() + "\nUpdated " + t5.ToLongTimeString();
                }
                if (Regex.IsMatch(t.Key, "^ld"))
                {
                    var comp = LDRText.GetComponent<Text>();
                    if (float.Parse(c.ToString()) > 350f)
                    {
                        comp.color = new Color(255, 0, 0);
                    }
                    else
                    {
                        comp.color = new Color(0, 0, 0);
                    }
                    t6 = DateTime.Now;
                    LDRText.SetActive(true);
                    comp.text = "LDR:" + c.ToString()+"\nUpdated "+t6.ToLongTimeString();
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