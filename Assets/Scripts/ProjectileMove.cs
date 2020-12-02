using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;

    public GameObject nuzzlePrefab;
    public GameObject hitPrefab;

    Vector3 targetPos;
    Vector3 originalPos;

    float interpolation = 0f;

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
            //transform.position += (targetPos - originalPos) * (speed * Time.deltaTime);
            transform.position = Vector3.Lerp(originalPos, targetPos, interpolation);
            interpolation += Time.deltaTime * speed;
        }
        else
        {
            Debug.Log("No speed");
        }

        if (interpolation >= 1f)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTargetPos(Vector3 targetPos, Vector3 originalPos)
    {
        this.targetPos = new Vector3(targetPos.x, targetPos.y + 2, targetPos.z); ;
        this.originalPos = originalPos;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<SimpleEnemyBehaviour>().Dead();

            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            if (hitPrefab != null)
            {
                var hitVFX = Instantiate(hitPrefab, pos, rot);
            }

            //Debug.Log("Se destruye el proyectil");
            Destroy(this.gameObject);
        }
       
    }



}
