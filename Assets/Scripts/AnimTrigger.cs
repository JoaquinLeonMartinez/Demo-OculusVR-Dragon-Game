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

                    StartCoroutine(WaitSeconds(0.5f));
                    SoundManager.Instance.footSteps.isRunning = true;
                }
                else if (animation == "IdleActive")//Significa que esta a punto de rugir y quedarse en idle
                {
                    SoundManager.Instance.footSteps.isRunning = false;
                    SoundManager.Instance.Play("Roar");
                }
                else if (animation == "StartGlide")
                {
                    SoundManager.Instance.footSteps.isFlying = false;
                }
                else if (animation == "RunAgain")
                {
                    SoundManager.Instance.footSteps.isRunning = true;
                }
                else if (animation == "TakeOff")
                {
                    SoundManager.Instance.footSteps.isRunning = false;
                    SoundManager.Instance.footSteps.isFlying = true;
                }
            }


            Destroy(this.gameObject);
        }
    }

    IEnumerator WaitSeconds(float pause)
    {
        yield return new WaitForSeconds(pause);
    }
}

