﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontEnemy : MonoBehaviour
{

    bool isFrontTarget = true;
    Transform target;
    float rotationSpeed = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        /*
        if (!isFrontTarget && target != null)
        {
            Quaternion.Lerp(origin.rotation, Quaternion.LookRotation(target.position), Time.deltaTime * rotationSpeed);


        }
        */

        if ( target != null && !isFrontTarget) // we get sure the target is here
        {
            var rotationAngle = Quaternion.LookRotation (target.position - transform.position); // we get the angle has to be rotated
            transform.rotation = Quaternion.Slerp ( transform.rotation, rotationAngle, Time.deltaTime * rotationSpeed); // we rotate the rotationAngle 
        }
        
    }

    public void FaceEnemy(Transform enemy) //puede ser solo el dragon y no el jugador tambien (ya que este ya se encuentra girado)
    {
        target = enemy;
        isFrontTarget = false;
        //transform.LookAt(enemy);
    }

    public void FaceEnemySimple(Transform enemy) //puede ser solo el dragon y no el jugador tambien (ya que este ya se encuentra girado)
    {
        transform.LookAt(enemy);
    }

}