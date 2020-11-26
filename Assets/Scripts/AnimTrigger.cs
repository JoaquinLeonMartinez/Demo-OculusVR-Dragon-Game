using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    [SerializeField] string animation;
    [SerializeField] DragonController dragonManager;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dragon"))
        {
            if (other.gameObject.GetComponent<Animator>() != null)
            {
                other.gameObject.GetComponent<Animator>().SetBool(animation, true);
            }

            if (dragonManager != null)
            {
                if (animation == "StartLanding")
                {
                    dragonManager.SetLanded(true);
                }
            }


            Destroy(this.gameObject);
        }
    }
}

