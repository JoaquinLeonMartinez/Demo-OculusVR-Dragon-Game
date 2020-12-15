using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTrigger : MonoBehaviour
{
    public DragonFollower follower;
    public GameObject enableTrigger;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        follower.SetSpeed(0);
        if (enableTrigger != null)
        {
            enableTrigger.SetActive(true);
        }
    }
}
