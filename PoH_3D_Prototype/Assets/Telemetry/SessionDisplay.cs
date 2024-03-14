using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SessionDisplay : MonoBehaviour
{
    public void OnConnectionSuccess(int sessionID)
    {
        var displayFeild = GetComponent<TextMeshProUGUI>();
        if (sessionID < 0)
        {
            displayFeild.text = $"Logging locally (Session {sessionID})";
        }
        else
        {
            displayFeild.text = $"Connected to Server (Session{sessionID})";
        }
    }

    public void OnConnecetionFail(string errorMessage)
    {
        var displayField = GetComponent<TextMeshProUGUI>();
        displayField.text = $"Error: {errorMessage}";
    }

}
