using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveFalse : MonoBehaviour
{
    public GameObject gameObject;
    void Start()
    {
        gameObject.SetActive(false);
    }
}
