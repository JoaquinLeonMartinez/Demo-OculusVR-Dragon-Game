using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject dragon;
   
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void SetPlayerOnDragon()
    {
        player.transform.parent = dragon.transform;
        player.transform.position = new Vector3(dragon.transform.position.x, dragon.transform.position.y + 1f, dragon.transform.position.z);
        player.transform.rotation = dragon.transform.rotation;
    }
}
