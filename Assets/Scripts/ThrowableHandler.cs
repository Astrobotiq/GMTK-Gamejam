using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowableHandler : MonoBehaviour
{
    BoxCollider2D collider;
    GameObject selectedObject;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        selectedObject = null;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (selectedObject == other.gameObject)
        {
            selectedObject = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Throwable>() != null || other.GetComponent<Meteor>() != null)
        {
            Debug.Log(other.gameObject.name);
            selectedObject = other.gameObject;
        }
    }

    public GameObject getSelected()
    {
        return selectedObject;
    }
    
}
