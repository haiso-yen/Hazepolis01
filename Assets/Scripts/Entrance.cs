using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public string entrancePassword;

    private void Start()
    {
        if(Eammon.instance.scenePassWord == entrancePassword)
        {
            Eammon.instance.transform.position = transform.position;
            Debug.Log("ENTER");
        }
        else
        {
            Debug.Log("WRONG");
        }
    }
}
