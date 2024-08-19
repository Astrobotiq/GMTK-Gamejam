using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTank : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(oxygenMover());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("OxygenManager"))
        {
            OxygenManager oxygenManager = other.GetComponent<OxygenManager>();
            if (oxygenManager != null)
            {
                oxygenManager.FillOxygen();
            }
            Destroy(this.gameObject);
        }
    }

    IEnumerator oxygenMover()
    {
        float time = 0;
        float duration = 1.5f;
        bool isLeft = true;
        Vector2 side;

        while (true)
        {
            if (time>duration)
            {
                rb.velocity = Vector2.zero;
                if (isLeft)
                {
                    
                    side = Vector2.left;
                    isLeft = !isLeft;
                }
                else
                {
                    side = Vector2.right;
                    isLeft = !isLeft;
                }
                rb.AddForce(side*0.8f,ForceMode2D.Impulse);
                time = 0;
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
}
