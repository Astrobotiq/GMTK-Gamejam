using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenUI : MonoBehaviour
{
    float oxygenLevel;
    public Image image;
    public List<Sprite> Images;
    // Start is called before the first frame update
    void Start()
    {
        oxygenLevel = 100f;
        setPicture();
    }

    public void setOxygenLevel(float level)
    {
        oxygenLevel = level;
        Debug.Log(oxygenLevel);
        setPicture();
    }

    void setPicture()
    {
        int imageNum;
        if (oxygenLevel >80)
        {
            imageNum = 0;
        }
        else if (oxygenLevel>60)
        {
            imageNum = 1;
        }else if (oxygenLevel>40)
        {
            imageNum = 2;
        }
        else if (oxygenLevel>25)
        {
            imageNum = 3;
        }
        else if (oxygenLevel>5)
        {
            imageNum = 4;
        }
        else
        {
            imageNum = 5;
        }
        image.sprite = Images[imageNum];
    }
}
