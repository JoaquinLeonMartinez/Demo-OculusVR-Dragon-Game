using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateProjectile : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();

    GameObject effectToSpawn;

    public float timerShoot = 1f;
    float timer = 1f;

    void Start()
    {
        effectToSpawn = vfx[0];
    }

    
    void Update()
    {
        if (timer <= 0)
        {
            SpawnVFX();
            timer = timerShoot;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void SpawnVFX()
    {
        GameObject vfx;
        if (firePoint != null)
        {
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("No fire point");
        }
    }
}
