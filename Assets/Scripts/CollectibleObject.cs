using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO: Determinar que hace esto
        if (other.CompareTag("Dragon"))
        {
            Destroy(this.gameObject);
        }
    }
}
