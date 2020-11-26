using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    [SerializeField] string animation;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dragon"))
        {
            if (other.gameObject.GetComponent<Animator>() != null)
            {
                other.gameObject.GetComponent<Animator>().SetBool(animation, true);
            }

            Destroy(this.gameObject);
        }
    }
}

