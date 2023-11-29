using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeToPlay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject leftToggle;
    public GameObject rightToggle;
    public GameObject leftHand;
    public GameObject rightHand;
    public bool leftFlag;
    public bool rightFlag;
    bool openTwoHand;
    public SpawnBall spawn;
    void Start()
    {
        leftFlag = leftToggle.GetComponent<Toggle>().isOn;
        rightFlag = rightToggle.GetComponent<Toggle>().isOn;
        openTwoHand = (leftFlag && rightFlag) ? true : false;
        spawn.setIsLeftHand(leftFlag);
        spawn.setIsRightHand(rightFlag);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setFlagLeftHand()
    {
        if (leftFlag)
        {
            leftHand.SetActive(false);
            leftFlag = false;
            spawn.setIsLeftHand(false);
        }
        else
        {
            leftHand.SetActive(true);
            leftFlag = true;
            spawn.setIsLeftHand(true);

        }
        checkFlag();
    }
    public void setFlagRightHand()
    {
        if (rightFlag)
        {
            rightHand.SetActive(false);
            rightFlag = false;
            spawn.setIsRightHand(false);
        }
        else
        {
            rightHand.SetActive(true);
            rightFlag = true;
            spawn.setIsRightHand(true);
        }
        checkFlag();
    }

    void checkFlag()
    {
        if (!rightFlag && !leftFlag)
        {
            setFlagLeftHand();
            setFlagRightHand();
            leftToggle.GetComponent<Toggle>().isOn = true;
            rightToggle.GetComponent<Toggle>().isOn = true;
        }
    }
}
