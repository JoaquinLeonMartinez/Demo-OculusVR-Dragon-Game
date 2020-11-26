using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CinematicsManager : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies;

    [SerializeField] GameObject dragon;

    [SerializeField] PathCreator landPath;
    [SerializeField] PathCreator runPath;
    [SerializeField] PathCreator takeOffPath;

    void Start()
    {
        
    }


    void Update()
    {

    }

    //Spawn enemies in scene
    public void SpawnEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy?.SetActive(true);
        }
    }

    //Aterrizar del dragon //Primera Cinematica
    public void InitDragon()
    {
        dragon.SetActive(true);
        dragon?.GetComponent<DragonFollower>().ChangePath(landPath);
    }

    //Correr, segunda parte de la primera cinematica
    public void RunDragon()
    {
        dragon?.GetComponent<DragonFollower>().ChangePath(runPath);
        //dragon?.GetComponent<Animator>().SetBool("StartRun", true);
    }

    //Despegar //Segunda Cinematica
    public void TakeOffDragon()
    {
        dragon?.GetComponent<DragonFollower>().ChangePath(takeOffPath);
        //TODO: Update animator
    }
}
