using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI displayField;
    public void OnConnectionSuccess(int sessionNumber)
    {
        
       
        if (sessionNumber < 0)
        {
            displayField.text = $"Logging Locally (Session #{sessionNumber})";

        }
        else
        {
            displayField.text = $"Connected to Server (Session #{sessionNumber})";
        }
    }

    public void OnConnectionFailure(string errorMessage)
    {
        displayField.text = $"Error #{errorMessage}";
    }
}
