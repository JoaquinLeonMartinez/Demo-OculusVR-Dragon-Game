using System.Collections;
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

    [SerializeField] CinematicsManager cinematicsManager;
    [SerializeField] GameManager gameManager;

    public bool isLanded;

    void Start()
    {
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
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }



        //Right hand inputs
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButtonValue) && rightPrimaryButtonValue)
        {
            //Debug.Log("Se esta pulsando el boton principal del mando derecho");
            cinematicsManager.InitDragon();
        }

        if (rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue) && rightTriggerValue > 0.1)
        {
            //Debug.Log($"Se esta pulsando el trigger derecho, value: {rightTriggerValue}");
            ThrowFireAttack();
        }


        if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightPrimaryAxisValue) && rightPrimaryAxisValue != Vector2.zero)
        {
            //Debug.Log("Se esta pulsando el joystick derecho");
        }

        //Left hand inputs
        if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftPrimaryButtonValue) && leftPrimaryButtonValue)
        {
            //Debug.Log("Se esta pulsando el boton principal del mando izquierdo");
            cinematicsManager.TakeOffDragon();
            
        }

        if (leftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue) && leftTriggerValue > 0.1)
        {
            //Debug.Log($"Se esta pulsando el trigger izquierdo, value: {leftTriggerValue}");
            cinematicsManager.SpawnEnemies();
        }

        if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftPrimaryAxisValue) && leftPrimaryAxisValue != Vector2.zero)
        {
            //Debug.Log("Se esta pulsando el joystick izquierdo");
        }

    }

    public void ThrowFireAttack()
    {
        //sourceTransform

        
        RaycastHit hit;

        if (Physics.Raycast(sourceTransform.position, sourceTransform.forward, out hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<SimpleEnemyBehaviour>().Dead();

            }
            else if (hit.collider.CompareTag("Dragon"))
            {
                //Montarse en el dragon
                if (isLanded)
                {
                    gameManager.SetPlayerOnDragon();
                    isLanded = false;
                }
               
            }
        }
        /*
        if (timer <= 0)
        {
            var aux = Instantiate(bullet, sourceTransform.position, sourceTransform.rotation);
            //aux.transform.forward = sourceTransform.forward;
            //Debug.Log("Se ha instanciado una bala");
            timer = cooldownFire;
        }
        */
    }

    public void SetLanded(bool isLanded)
    {
        this.isLanded = isLanded;
    }

}
