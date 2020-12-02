using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontEnemy : MonoBehaviour
{

    bool isFrontTarget = true;
    Transform target;
    float rotationSpeed = 2f;
    Quaternion originalRotation;
    bool firstShoot = false;

    [SerializeField] GameObject gameManager;

    void Start()
    {
        originalRotation = transform.rotation;
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
            
            var rotationAngle = Quaternion.LookRotation(target.position - transform.position); // we get the angle has to be rotated //target.position - transform.position
            transform.rotation = Quaternion.Slerp (transform.rotation, rotationAngle, Time.deltaTime * rotationSpeed); // we rotate the rotationAngle 
        }
        else if (target == null && firstShoot && gameManager.GetComponent<DragonController>().isLanded == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * rotationSpeed);
        }

        //Debug.Log($"Dragon rotation: {transform.rotation}");
        
    }

    public void FaceEnemy(Transform enemy) //puede ser solo el dragon y no el jugador tambien (ya que este ya se encuentra girado)
    {
        //guardar rotacion original antes de nada:
        originalRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z , transform.rotation.w);

        target = enemy;
        //target.position = new Vector3(target.position.x, 0, target.position.z);
        isFrontTarget = false;
        firstShoot = true;
        //transform.LookAt(enemy);
    }

    public void FaceEnemySimple(Transform enemy) //puede ser solo el dragon y no el jugador tambien (ya que este ya se encuentra girado)
    {
        transform.LookAt(enemy);
    }

}
