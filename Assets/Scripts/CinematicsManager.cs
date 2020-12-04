using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CinematicsManager : MonoBehaviour
{
    [SerializeField] GameObject enemiesParent;
    //[SerializeField] List<GameObject> enemies;

    //[SerializeField] GameObject dragon;

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
    public float takeOffDelay = 3f;
    float takeOffTimer;
    void Start()
    {
        takeOffTimer = takeOffDelay;
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
        if (!playerOnDragon && gameManager.GetComponent<GameManager>().dragonParent?.GetComponent<DragonFollower>().distanceTravelled >= gameManager.GetComponent<GameManager>().dragonParent?.GetComponent<DragonFollower>().pathCreator.path.length && gameManager?.GetComponent<DragonController>().isLanded == true) 
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
            //Delay here
            if (takeOffTimer <= 0)
            {
                TakeOffDragon();
                takeOffTimer = takeOffDelay;
            }
            else
            {
                takeOffTimer -= Time.deltaTime;
            }
            
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
        gameManager.GetComponent<GameManager>().dragonParent.SetActive(true);
        gameManager.GetComponent<GameManager>().dragonParent.GetComponent<DragonFollower>().speed = 5;
        gameManager.GetComponent<GameManager>().dragonParent?.GetComponent<DragonFollower>().ChangePath(landPath);
        SoundManager.Instance.Play("Roar");
        SoundManager.Instance.footSteps.isFlying = true;

        //TODO: Cuando aterrice se debe hacer la transicion de la camara
    }

    //Correr, segunda parte de la primera cinematica
    public void RunDragon()
    {
        gameManager.GetComponent<GameManager>().dragonParent?.GetComponent<DragonFollower>().ChangePath(runPath);
        //dragon?.GetComponent<Animator>().SetBool("StartRun", true);
    }

    //Despegar //Segunda Cinematica
    public void TakeOffDragon()
    {
        gameManager.GetComponent<DragonController>().isBattleEnded = true;
        gameManager.GetComponent<GameManager>().dragon.GetComponent<FrontEnemy>().FaceEnemy(takeOffTargetPoint.transform);
        gameManager?.GetComponent<GameManager>().SetPlayerOnDragon();
        gameManager.GetComponent<GameManager>().dragonParent?.GetComponent<DragonFollower>().ChangePath(takeOffPath);
        gameManager.GetComponent<DragonController>().isLanded = false;
        gameManager.GetComponent<GameManager>().dragon?.GetComponent<Animator>().SetBool("RunAgain", true);
        SoundManager.Instance.footSteps.isRunning = true;
        gameManager.GetComponent<GameManager>().dragon?.gameObject.GetComponent<Animator>().SetBool("StartLanding", false);
        gameManager.GetComponent<GameManager>().dragon?.gameObject.GetComponent<Animator>().SetBool("IdleActive", false);
        gameManager.GetComponent<GameManager>().dragon?.gameObject.GetComponent<Animator>().SetBool("StartGlide", false);
        gameManager.GetComponent<GameManager>().dragon?.gameObject.GetComponent<Animator>().SetBool("TakeOff", false);
    }
}
