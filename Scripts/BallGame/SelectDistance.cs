using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectDistance : MonoBehaviour
{
    TMP_Dropdown dropdown; // Reference to your Dropdown UI element.
    public SpawnBall spawn;
    private void Start()
    {
        // Subscribe to the Dropdown's OnValueChanged event.
        dropdown = GetComponent<TMP_Dropdown>();
        if(dropdown == null){
            Debug.Log("test");
        }
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDropdownValueChanged(int newValue)
    {
        // Get the selected value from the Dropdown's options.
        string selectedValue = dropdown.options[newValue].text;
        Debug.Log(selectedValue);
        spawn.setPosZ(selectedValue);
    }
}
