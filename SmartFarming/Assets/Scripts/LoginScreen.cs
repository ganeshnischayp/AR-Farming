using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
public class LoginScreen : MonoBehaviour {
    public GameObject email;
    public GameObject password;
    public Text DebugText;
    private string Email;
    private bool val;
    private string dtext;
    private string Password;
    private string obtained;
    public const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        public static bool validateEmail(string email){
        if (email != null)
            return Regex.IsMatch(email, MatchEmailPattern);
        else
            return false;
    }
    public void Login()
    {
        val = true ;
        Email = email.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        obtained = PlayerPrefs.GetString(Email, "");

        if(!validateEmail(Email))
        {
            val = false;
            dtext="Email address was incorrect! Try Again..";
        }
        else if(obtained=="")
        {
            val = false;
            dtext="Email address is not registered! Click Register..";
        }
        else if(Password.Length==0||obtained!=Password)
        {
            val = false;
            dtext="Password was incorrect! Try Again..";
        }
        if (val)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            DebugText.text = dtext;
        }
    }
    public void Register()
    {
        val = true;
        Email = email.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        obtained = PlayerPrefs.GetString(Email, "");

        if (!validateEmail(Email))
        {
            val = false;
            dtext = "Email address was incorrect! Try Again..";
        }
        else if (Password.Length == 0)
        {
            val = false;
            dtext = "Password was incorrect! Try Again..";
        }
        else if (obtained != "")
        {
            val = false;
            dtext = "Email address is already registered! Please Login";
        }
        if(val)
        {
            PlayerPrefs.SetString(Email,Password);
            SceneManager.LoadScene("Main");
        }
        else
        {
            DebugText.text = dtext;
        }
    }
}
