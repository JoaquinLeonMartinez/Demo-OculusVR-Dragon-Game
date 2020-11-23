﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DragonController : MonoBehaviour
{
    InputDevice rightController;
    InputDevice leftController;


    void Start()
    {
        
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics desiredDevide = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredDevide, devices); //Con esto obtenemos todos los inputs de las Quest

        if (devices.Count > 0)
        {
           rightController = devices[0];
            Debug.Log($"Name: {rightController.name} -- Characteristics: {rightController.characteristics}");
        }
       

        desiredDevide = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredDevide, devices); //Con esto obtenemos todos los inputs de las Quest

        if (devices.Count > 0)
        {
            leftController = devices[0];
            Debug.Log($"Name: {leftController.name} -- Characteristics: {leftController.characteristics}");
        }
            
    }

    void Update()
    {
        //Right hand inputs
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButtonValue) && rightPrimaryButtonValue)
        {
            Debug.Log("Se esta pulsando el boton principal del mando derecho");
        }

        if (rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue) && rightTriggerValue > 0.1)
        {
            Debug.Log($"Se esta pulsando el trigger derecho, value: {rightTriggerValue}");
        }

        
        if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightPrimaryAxisValue) && rightPrimaryAxisValue != Vector2.zero)
        {
            Debug.Log("Se esta pulsando el joystick derecho");
        }

        //Left hand inputs
        if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftPrimaryButtonValue) && leftPrimaryButtonValue)
        {
            Debug.Log("Se esta pulsando el boton principal del mando izquierdo");
        }

        if (leftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue) && leftTriggerValue > 0.1)
        {
            Debug.Log($"Se esta pulsando el trigger izquierdo, value: {leftTriggerValue}");
        }

        if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftPrimaryAxisValue) && leftPrimaryAxisValue != Vector2.zero)
        {
            Debug.Log("Se esta pulsando el joystick izquierdo");
        }

    }
}