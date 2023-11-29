using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBallLocation : MonoBehaviour
{  
    Button button;
    public Text buttonText;
    public bool isSelect = false;
    public float x = 0f;
    public float y = 0f;
    void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<Text>();

        button.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void OnButtonClick(){
        if(!isSelect){
            isSelect = true;
            buttonText.text = "true";
        }
        else{
            isSelect = false;
            buttonText.text = "false";
        }
    }

    public float getX(){
        return x;
    }

    public float getY(){
        return y;
    }

    public bool getSelect(){
        return isSelect;
    }

    public void setSelect(bool isSelect){
        this.isSelect = isSelect;
    }
}
