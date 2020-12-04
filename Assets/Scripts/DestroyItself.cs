using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItself : MonoBehaviour
{
    public float destroyTimer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyTimer <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            destroyTimer -= Time.deltaTime;
        }
    }
}
