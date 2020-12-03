using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSecondPartTrigger : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public void OnTriggerEnter(Collider other)
    {
        //dragon.dragonMovementActive = true;
        gameManager.dragon.GetComponent<PlayerDragonController>().isPlayerControllerActive = true;
    }
}
