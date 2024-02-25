using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectDistance : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Reference to your Dropdown UI element.
    public SpawnBall spawn;
    private void Start()
    {
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        spawn.setPosZ("near");
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

    public void SetDropdownToDefault()
    {
        dropdown.value = 0; // Set to the index of the default item
    }
}
