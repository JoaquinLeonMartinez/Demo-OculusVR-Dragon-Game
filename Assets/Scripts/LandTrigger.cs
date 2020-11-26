using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dragon"))
        {
            if (other.gameObject.GetComponent<Animator>() != null)
            {
                other.gameObject.GetComponent<Animator>().SetBool("StartLanding", true);
            }

            Destroy(this.gameObject);
        }
    }
}
