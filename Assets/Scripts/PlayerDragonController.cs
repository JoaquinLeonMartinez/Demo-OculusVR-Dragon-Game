using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class PlayerDragonController : MonoBehaviour
{
    InputDevice rightController;
    public bool isPlayerControllerActive = false;
    public float playerSpeed = 0.001f;
    float thresholdMovement = 6f;

    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics desiredDevide = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredDevide, devices); //Con esto obtenemos todos los inputs de las Quest

        if (devices.Count > 0)
        {
            rightController = devices[0];
            //Debug.Log($"Name: {rightController.name} -- Characteristics: {rightController.characteristics}");
        }
    }

    void Update()
    {
        //Debug.Log($"La posicion local del dragon es: x: {transform.localPosition.x} y: {transform.localPosition.y} z: {transform.localPosition.z}");
        if (isPlayerControllerActive)
        {
            if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightPrimaryAxisValue))
            {
                //Debug.Log("Se esta pulsando el joystick derecho");
                //Debug.Log($"Se esta pulsando el joystick derecho, posicion: x: {rightPrimaryAxisValue.x} y: {rightPrimaryAxisValue.y}");
                //Debug.Log($"La posicion local del dragon es: x: {transform.localPosition.x} y: {transform.localPosition.y} z: {transform.localPosition.z}");

                if (rightPrimaryAxisValue.x != 0) 
                {
                    //Nos movemos hacia donde indique el joystick

                    if ((transform.localPosition.x < thresholdMovement && rightPrimaryAxisValue.x > 0)) //derecha
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x + rightPrimaryAxisValue.x * Time.deltaTime * playerSpeed, transform.localPosition.y, transform.localPosition.z);
                    }
                    else if ((rightPrimaryAxisValue.x < 0 && transform.localPosition.x > -thresholdMovement)) //izq
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x + rightPrimaryAxisValue.x * Time.deltaTime * playerSpeed, transform.localPosition.y, transform.localPosition.z);
                    }
                }
                this.gameObject.GetComponent<Animator>().SetFloat("TurnDragon", rightPrimaryAxisValue.x);
            }
        }
    }

    /*
    if (player.transform.localPosition.x < 3 || player.transform.localPosition.x > -3)
    {
        Debug.Log("Se esta moviendo");
        //transform.position += new Vector3(transform.forward.x, 0, 0) * Time.deltaTime * frontSpeed;
        //transform.localPosition += transform.forward * Time.deltaTime * frontSpeed;
    }

 */
}
