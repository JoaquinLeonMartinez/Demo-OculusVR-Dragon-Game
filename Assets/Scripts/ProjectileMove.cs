using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;

    public GameObject nuzzlePrefab;
    public GameObject hitPrefab;

    void Start()
    {
        if (nuzzlePrefab != null)
        {
            var nuzzleVFX = Instantiate(nuzzlePrefab, transform.position, Quaternion.identity);
            nuzzleVFX.transform.forward = gameObject.transform.forward;
        }
    }

    void Update()
    {
        if (speed != 0f)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No speed");
        }
    }



}
