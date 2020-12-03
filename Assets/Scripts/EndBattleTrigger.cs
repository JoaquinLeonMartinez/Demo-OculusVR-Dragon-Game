using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattleTrigger : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public void OnTriggerEnter(Collider other)
    {
        gameManager.dragon.GetComponent<PlayerDragonController>().isPlayerControllerActive = true;
    }
}
