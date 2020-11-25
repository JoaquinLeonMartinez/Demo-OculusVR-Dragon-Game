using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public float lifetime;
    void Start()
    {
        lifetime = 10f;
        speed = 200f;
        this.GetComponent<Rigidbody>().AddForce(this.transform.forward * speed);
    }

    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.gameObject.GetComponent<SimpleEnemyBehaviour>().Dead();

            Destroy(this.gameObject);
        }
    }
}

