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
    void Start()
    {
        
    }

    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
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
