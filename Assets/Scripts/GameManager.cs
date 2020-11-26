using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject dragon;
    [SerializeField] GameObject playerPoint;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void SetPlayerOnDragon()
    {
        player.transform.parent = dragon.transform;
        player.transform.position = playerPoint.transform.position;
        player.transform.rotation = dragon.transform.rotation;
    }
}
