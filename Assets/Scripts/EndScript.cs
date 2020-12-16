using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public GameObject GameManager;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.GetComponent<CinematicsManager>().explication = Explication.Explication_3;
        Destroy(this.gameObject);
    }
}
