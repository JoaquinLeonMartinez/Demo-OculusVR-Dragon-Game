using UnityEngine;
using System.Collections;


public class CameraLookAt : MonoBehaviour
{

    public Transform target; //Look at
    private Transform aimTarget;
    private Vector3 aimAngle = Vector3.zero; 
    private Vector3 finalAngle = Vector3.zero; 

    void Start()
    {
       
        GameObject go = new GameObject("CameraLookAt AimTarget");
        go.transform.parent = transform.parent;
        go.transform.position = transform.position;
        aimTarget = go.transform;
        aimAngle = finalAngle = transform.eulerAngles;
    }


    void LateUpdate()
    {
        aimTarget.LookAt(target, Vector3.up); 
        aimAngle = aimTarget.eulerAngles; 

        
        while (aimAngle.y - finalAngle.y > 180f) aimAngle.y -= 360f;
        while (aimAngle.y - finalAngle.y < -180f) aimAngle.y += 360f;

        finalAngle = Vector3.Lerp(finalAngle, aimAngle, Time.deltaTime * 2f); 

        
        while (finalAngle.y < 0f) finalAngle.y += 360f; 
        while (finalAngle.y > 360f) finalAngle.y -= 360f; 

        transform.eulerAngles = finalAngle; 
    }

   
    public void LookAtMe(Transform t)
    {
        target = t;
    }
}


