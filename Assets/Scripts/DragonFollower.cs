using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class DragonFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    public float distanceTravelled;
    public bool isCamera = false;


    void Start()
    {

    }

    void Update()
    {
        if (speed > 0 && distanceTravelled < pathCreator.path.length)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            //transform.position = new Vector3(transform.position.x + thresholdMovement, transform.position.y, transform.position.z);
            if (!isCamera)
            {
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }
    }

    public void ResetFollower()
    {
        distanceTravelled = 0;
    }

    public void ChangePath(PathCreator newPathCreator)
    {
        pathCreator = newPathCreator;
        ResetFollower();
    }

    public Quaternion getActualPathRotation()
    {
        return pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
    }

    public Vector3 getCurrentPathCenter()
    {
        return pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
    }

    public float GetPathLenght()
    {
        return pathCreator.path.length;
    }
}
