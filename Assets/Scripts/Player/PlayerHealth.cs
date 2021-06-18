using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image FullBar1;
    public Image FullBar2;
    public Image FullBar3;
    public Image FullBar4;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.Health >= 4)
        {
            FullBar1.enabled = true;
            FullBar2.enabled = true;
            FullBar3.enabled = true;
            FullBar4.enabled = true;
        }
        else if (GameManager.instance.Health == 3)
        {
            FullBar1.enabled = true;
            FullBar2.enabled = true;
            FullBar3.enabled = true;
            FullBar4.enabled = false;
        }
        else if (GameManager.instance.Health == 2)
        {
            FullBar1.enabled = true;
            FullBar2.enabled = true;
            FullBar3.enabled = false;
            FullBar4.enabled = false;
        }
        else if (GameManager.instance.Health == 1)
        {
            FullBar1.enabled = true;
            FullBar2.enabled = false;
            FullBar3.enabled = false;
            FullBar4.enabled = false;
        }
        else if (GameManager.instance.Health <= 0)
        {
            FullBar1.enabled = false;
            FullBar2.enabled = false;
            FullBar3.enabled = false;
            FullBar4.enabled = false;
        }
    }
}
