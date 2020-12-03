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

                if (dragonManager != null) //desactivamos todas las animacions
                {
                    other.gameObject.GetComponent<Animator>().SetBool("StartLanding", false);
                    other.gameObject.GetComponent<Animator>().SetBool("IdleActive", false);
                    other.gameObject.GetComponent<Animator>().SetBool("StartGlide", false);
                    other.gameObject.GetComponent<Animator>().SetBool("TakeOff", false);
                    other.gameObject.GetComponent<Animator>().SetBool("RunAgain", false);
                }
                //activamos la que queremos
                other.gameObject.GetComponent<Animator>().SetBool(animation, true);
            }

            if (dragonManager != null)
            {
                if (animation == "StartLanding")
                {
                    dragonManager.SetLanded(true);
                }

                //Significa que esta a punto de rugir y quedarse en idle
                if (animation == "IdleActive")
                {
                    SoundManager.Instance.Play("Roar");
                }
            }


            Destroy(this.gameObject);
        }
    }
}

