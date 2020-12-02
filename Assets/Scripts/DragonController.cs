﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DragonController : MonoBehaviour
{
    InputDevice rightController;
    InputDevice leftController;

    [SerializeField] Transform sourceTransform;
    [SerializeField] GameObject bullet;
    public float cooldownFire;
    float timer;
    bool readyToAttack;
    public bool isOnDragon;

    [SerializeField] CinematicsManager cinematicsManager;
    [SerializeField] GameManager gameManager;

    public bool isLanded;

    void Start()
    {
        isOnDragon = false;
        readyToAttack = true;
        isLanded = false;
        cooldownFire = 0.5f;
        timer = 0;
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics desiredDevide = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredDevide, devices); //Con esto obtenemos todos los inputs de las Quest

        if (devices.Count > 0)
        {
           rightController = devices[0];
            //Debug.Log($"Name: {rightController.name} -- Characteristics: {rightController.characteristics}");
        }
       

        desiredDevide = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredDevide, devices); //Con esto obtenemos todos los inputs de las Quest

        if (devices.Count > 0)
        {
            leftController = devices[0];
            //Debug.Log($"Name: {leftController.name} -- Characteristics: {leftController.characteristics}");
        }
            
    }

    void Update()
    {
        //update timer
        if (timer > 0 && readyToAttack == false)
        {
            gameManager.dragon.GetComponent<Animator>().SetBool("Attack", false);
            timer -= Time.deltaTime;
        }
        else if(readyToAttack == false)
        {
            readyToAttack = true;
            timer = cooldownFire;
        }



        //Right hand inputs
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButtonValue) && rightPrimaryButtonValue)
        {
            //Debug.Log("Se esta pulsando el boton principal del mando derecho");
            //cinematicsManager.InitDragon();
        }

        if (rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue) && rightTriggerValue > 0.1)
        {
            //Debug.Log($"Se esta pulsando el trigger derecho, value: {rightTriggerValue}");
            if (readyToAttack && isOnDragon)
            {
                ThrowFireAttack();
            }
            
        }


        if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightPrimaryAxisValue) && rightPrimaryAxisValue != Vector2.zero)
        {
            //Debug.Log("Se esta pulsando el joystick derecho");
        }

        //Left hand inputs
        if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftPrimaryButtonValue) && leftPrimaryButtonValue)
        {
            //Debug.Log("Se esta pulsando el boton principal del mando izquierdo");
            //cinematicsManager.TakeOffDragon();
            
        }

        if (leftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue) && leftTriggerValue > 0.1)
        {
            //Debug.Log($"Se esta pulsando el trigger izquierdo, value: {leftTriggerValue}");
            //cinematicsManager.SpawnEnemies();
        }

        if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftPrimaryAxisValue) && leftPrimaryAxisValue != Vector2.zero)
        {
            //Debug.Log("Se esta pulsando el joystick izquierdo");
        }

    }

    public void ThrowFireAttack()
    {
        RaycastHit hit;

        if (Physics.Raycast(sourceTransform.position, sourceTransform.forward, out hit, LayerMask.GetMask("Enemies")))
        {

            if (hit.collider.CompareTag("Enemy"))
            {
                readyToAttack = false; //solo hay cooldown cuando acierta en un enemigo
                gameManager.dragon.GetComponent<FrontEnemy>().FaceEnemy(hit.collider.transform);
                gameManager.dragon.GetComponent<Animator>().SetBool("Attack", true);
                gameManager.dragon.GetComponent<CreateProjectile>().SpawnVFX(hit.transform.position);
                //dragon anim 
                //hit.collider.gameObject.GetComponent<SimpleEnemyBehaviour>().Dead(); //ahora esto se hace en la bala
            }
        }
    }

    public void SetLanded(bool isLanded)
    {
        this.isLanded = isLanded;
    }

}
