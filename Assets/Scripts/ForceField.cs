using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var position =collision.transform.position;
        var direction = position-transform.position;
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-direction, ForceMode2D.Impulse);
        }
        
    }
}
