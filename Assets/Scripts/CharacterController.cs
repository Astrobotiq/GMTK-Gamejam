using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    bool canBlowOx;
    bool canRotateHead;
    bool isHolding;
    bool isBlowing;
    bool isLookingLeft;
    

    public Transform head;
    
    public enum States
    {
        Holding, Idle, Blowing
    }

    public States state;
    // Start is called before the first frame update
    void Start()
    {
        state = States.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Look(Vector2 dir)
    {
        if (dir.x > 0)
        {
            head.rotation = new Quaternion(0f,0f,0f,0f);
        }
        else if (dir.x < 0)
        {
            head.rotation = new Quaternion(0f,180f,0f,0f);
        }
        
    }

    public void Hold()
    {
        
    }
    
    
}
