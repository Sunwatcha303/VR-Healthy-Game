using UnityEngine;
using TMPro;

public class AlertMessage : MonoBehaviour
{
    public GameObject alertText;

    void Start()
    {
        // Hide the alert message initially
        alertText.SetActive(false);
    }

    public void ShowAlert(string message, float duration)
    {
        // Display the alert message
        TMP_Text textComponent = alertText.GetComponent<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = message;
            alertText.SetActive(true);

            // Hide the alert message after the specified duration
            Invoke("HideAlert", duration);
        }
        else
        {
            Debug.LogError("No TMP_Text component found on the alertText GameObject.");
        }
    }

    void HideAlert()
    {
        // Hide the alert message
        alertText.SetActive(false);
    }
}
