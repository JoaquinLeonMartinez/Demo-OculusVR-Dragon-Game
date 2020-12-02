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
        /*
        if (timer <= 0)
        {
            SpawnVFX();//firePoint
            timer = timerShoot;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        */
    }

    public void SpawnVFX(Vector3 targetPos)//GameObject firePoint
    {
        Debug.Log("Se crea el proyectil");
        GameObject vfx;
        if (firePoint != null)
        {
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            vfx.GetComponent<ProjectileMove>().SetTargetPos(targetPos, firePoint.transform.position);
        }
        else
        {
            Debug.Log("No fire point");
        }
    }
}
