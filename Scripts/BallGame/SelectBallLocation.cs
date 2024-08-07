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
    public Color yellow = new Color32(255, 178, 0, 255);
    public Color grey = new Color32(255, 255, 255, 255);
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
            buttonText.text = "On";
            button.GetComponent<Image>().color = yellow;
        }
        else{
            isSelect = false;
            buttonText.text = "Off";
            button.GetComponent<Image>().color = grey;
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
