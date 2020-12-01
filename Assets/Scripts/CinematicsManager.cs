﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CinematicsManager : MonoBehaviour
{
    [SerializeField] GameObject enemiesParent;
    //[SerializeField] List<GameObject> enemies;

    [SerializeField] GameObject dragon;

    [SerializeField] PathCreator landPath;
    [SerializeField] PathCreator runPath;
    [SerializeField] PathCreator takeOffPath;

    [SerializeField] GameObject cameraPlayer;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject takeOffTargetPoint;

    bool playerOnDragon = false;
    bool initedDragon = false;
    //bool allEnemiesDied = false;
    float roarTimer = 3f;
    float initDragonOpacity = 0;
    float opacitySpeed = 2f;
    float enemiesStartTimer = 3f; //esto pasara a ser un bool de cuando hayan acabado los "insultos"


    void Start()
    {

    }


    void Update()
    {

        if (enemiesStartTimer <= 0 && !initedDragon)
        {
            InitDragon();
        }
        else
        {
            enemiesStartTimer -= Time.deltaTime;
        }

        if (cameraPlayer.GetComponent<DragonFollower>().distanceTravelled >= cameraPlayer.GetComponent<DragonFollower>().pathCreator.path.length && gameManager.GetComponent<DragonController>().isOnDragon ==false)
        {
            gameManager.GetComponent<DragonController>().isOnDragon = true;
        }

        //si ya ha llegado a su destino y esta en tierra --> activamos la transicion de la camara
        if (!playerOnDragon && dragon?.GetComponent<DragonFollower>().distanceTravelled >= dragon?.GetComponent<DragonFollower>().pathCreator.path.length && gameManager?.GetComponent<DragonController>().isLanded == true) 
        {
            if (roarTimer <= 0)
            {
                playerOnDragon = true;
                //gameManager?.GetComponent<GameManager>().SetPlayerOnDragon();
                cameraPlayer.GetComponent<DragonFollower>().speed = 2f;
            }
            else
            {
                roarTimer -= Time.deltaTime;
            }
        }

        if (enemiesParent.transform.childCount == 0 && gameManager.GetComponent<DragonController>().isLanded == true) //si han muerto todos los enemigos
        {
            TakeOffDragon();
        }

    }

    //Spawn enemies in scene
    /*public void SpawnEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }
    }*/

    //Aterrizar del dragon //Primera Cinematica
    public void InitDragon()
    {
        initedDragon = true;
        dragon.SetActive(true);
        dragon.GetComponent<DragonFollower>().speed = 5;
        dragon?.GetComponent<DragonFollower>().ChangePath(landPath);

        //TODO: Cuando aterrice se debe hacer la transicion de la camara
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
        dragon.GetComponent<FrontEnemy>().FaceEnemy(takeOffTargetPoint.transform);
        gameManager?.GetComponent<GameManager>().SetPlayerOnDragon();
        dragon?.GetComponent<DragonFollower>().ChangePath(takeOffPath);
        gameManager.GetComponent<DragonController>().isLanded = false;
        dragon?.GetComponent<Animator>().SetBool("RunAgain", true);
        dragon?.gameObject.GetComponent<Animator>().SetBool("StartLanding", false);
        dragon?.gameObject.GetComponent<Animator>().SetBool("IdleActive", false);
        dragon?.gameObject.GetComponent<Animator>().SetBool("StartGlide", false);
        dragon?.gameObject.GetComponent<Animator>().SetBool("TakeOff", false);
    }
}