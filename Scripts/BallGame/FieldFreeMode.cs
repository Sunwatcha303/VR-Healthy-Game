using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldFreeMode : MonoBehaviour
{
    // Start is called before the first frame update
    public float positionX = 0;
    public float positionY = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 getPosition()
    {
        return new Vector2(positionX, positionY);
    }
}
