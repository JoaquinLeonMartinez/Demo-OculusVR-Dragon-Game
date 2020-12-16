using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public enum Explication { Explication_1, Explication_2, Explication_3, Explication_4, none };
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
    float endAppTimer;
    float secondExplicationTimer;
    bool isEnd;
    bool isSecondPartPlayed;
    float firstExplicationTimer;
    float firstExplicationTimer2;
    bool secondExplicationEnd;
    float enemyAudioDuration;
    int numOfEnemiesTalkingFirstRound;
    int numOfEnemiesTalkingSecondRound;
    public float takeOffDelay = 3f;
    float takeOffTimer;
    public Explication explication; //podriamos hacer de esto un entero o un enum para todas las explicaciones que vaya a haber
    void Start()
    {
        isEnd = false;
        secondExplicationEnd = false;
        isSecondPartPlayed = false;
        takeOffTimer = takeOffDelay;
        explication = Explication.Explication_1;
        firstExplicationTimer = SoundManager.Instance.GetLength("Explication_1") + 0.2f;
        firstExplicationTimer2 = SoundManager.Instance.GetLength("Explication_1_second") + 0.2f;
        endAppTimer = SoundManager.Instance.GetLength("Explication_3") + 1f;
        secondExplicationTimer = SoundManager.Instance.GetLength("Explication_2") + 0.1f;
        numOfEnemiesTalkingFirstRound = 4;
        numOfEnemiesTalkingSecondRound = 4;
    }


    void Update()
    {

        //Audio de Irene
        PlayExplication();

        //Audio de enemigos
        if (firstExplicationTimer <= 0 && !initedDragon)
        {
            //Antes de iniciar el dragon, los enemigos deben decir su frase
            if (numOfEnemiesTalkingFirstRound > 0)
            {
                if (enemyAudioDuration <= 0)
                {
                    enemyAudioDuration = SoundManager.Instance.InvokeEnemiesAudios();
                    numOfEnemiesTalkingFirstRound--;
                }
            }
            else if(enemyAudioDuration <= 0) //de esta forma se espera a que el ultimo acabe de hablar para aparecer
            {
                //Al finalizar el audio de los enemigos comienza esto:
                InitDragon();
            }

            enemyAudioDuration -= Time.deltaTime;
        }
        else
        {
            firstExplicationTimer -= Time.deltaTime;
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


        //Cuando ya estas encima del dragon
        if (cameraPlayer.GetComponent<DragonFollower>().distanceTravelled >= cameraPlayer.GetComponent<DragonFollower>().pathCreator.path.length && gameManager.GetComponent<DragonController>().isOnDragon ==false)
        {
            if (!isSecondPartPlayed)
            {
                explication = Explication.Explication_4;
            }

            //Antes de decir que estas sobre el dragon (lo cual indicara que puedes disparar) hay que poner la segunda parte del primer audio
            if (firstExplicationTimer2 <= 0)
            {
                gameManager.GetComponent<DragonController>().isOnDragon = true;
            }
            else
            {
                firstExplicationTimer2 -= Time.deltaTime;
            }  
        }

        //Cuando quedan la mitad de enemigos
        if (enemiesParent.transform.childCount == 4 && gameManager.GetComponent<DragonController>().isLanded == true)
        {
            //se activa el audio de los 4 enemigos que faltan y se desactiva el disparo hasta que acaban
            if (numOfEnemiesTalkingSecondRound > 0)
            {
                if (enemyAudioDuration <= 0)
                {
                    enemyAudioDuration = SoundManager.Instance.InvokeEnemiesAudios();
                    numOfEnemiesTalkingSecondRound--;
                    gameManager.GetComponent<DragonController>().isOnDragon = false;
                }
            }
            else if (enemyAudioDuration <= 0) //de esta forma se espera a que el ultimo acabe de hablar para aparecer
            {
                //Al finalizar el audio de los enemigos comienza esto:
                gameManager.GetComponent<DragonController>().isOnDragon = true;
            }

            enemyAudioDuration -= Time.deltaTime;
        }


        //si han muerto todos los enemigos
        if (enemiesParent.transform.childCount == 0 && gameManager.GetComponent<DragonController>().isLanded == true) 
        {
            if (!secondExplicationEnd)
            {
                explication = Explication.Explication_2;
            }

            if (secondExplicationTimer <= 0)
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
            else
            {
                secondExplicationTimer -= Time.deltaTime;
            }
        }

        if (isEnd)
        {
            if (endAppTimer <= 0)
            {
                Application.Quit(); //en un futuro podemos poner un menu o algo asi
            }
            else
            {
                endAppTimer -= Time.deltaTime;
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
        //SoundManager.Instance.Play("Roar");
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

    public void PlayExplication()
    {
        if (explication == Explication.Explication_1)
        {
            explication = Explication.none;
            SoundManager.Instance.Play("Explication_1");
        }
        else if (explication == Explication.Explication_2)
        {
            explication = Explication.none;
            SoundManager.Instance.Play("Explication_2");
            secondExplicationEnd = true;
        }
        else if (explication == Explication.Explication_3)
        {
            explication = Explication.none;
            SoundManager.Instance.Play("Explication_3");
            isEnd = true;
        }
        else if (explication == Explication.Explication_4)
        {
            explication = Explication.none;
            SoundManager.Instance.Play("Explication_1_second");
            isSecondPartPlayed = true;
        }
    }
}
