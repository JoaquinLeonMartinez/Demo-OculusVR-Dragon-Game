using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBehaviour : MonoBehaviour
{
    [SerializeField] bool isAlive;
    
    [SerializeField] float deathDelay;

    float deathTimer;

    //Animation parameters:
    Animator anim;
    float attackTime;
    float damageTime;
    float deathTime = 0;
    float idleTime;

    [SerializeField] Renderer renderSelectable;

    //public Shader shaderUnselected;
    //public Shader shaderSelected;
    //public Renderer rend;

    float opacity;
    bool isSelected;

    void Start()
    {
        /*
        rend = this.GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogError("No se ha encontrado el componente renderer hulio");
        }
        */
        isSelected = false;

        //shaderUnselected = Shader.Find("Legacy Shaders/Diffuse");
        //shaderSelected = Shader.Find("Legacy Shaders/Self-Illumin/Diffuse");

        isAlive = true;
        deathDelay = 0.01f;
        anim = this.GetComponent<Animator>();
        if (anim != null)
        {
            UpdateAnimClipTimes();
            deathTimer = deathTime + deathDelay;
        }
        else
        {
            Debug.LogError("No hay animator en el enemigo");
        }
        
    }

    
    void Update()
    {
        if (!isAlive)
        {
            OnDead();
        }

    }

    private void OnDead() //Internal method
    {
        if (deathTimer > 0f)
        {
            deathTimer -= Time.deltaTime;
            /*if (deathTimer <= deathDelay) //esto quiere decir que la animacion ha acabado y ya esta en el suelo
            {
                opacity = deathTimer / deathDelay;
                foreach (Transform child in this.transform)
                {
                    if (child.gameObject.GetComponent<Renderer>() != null)
                    {
                        var col = child.gameObject.GetComponent<Renderer>().material.color;
                        col.a = opacity;
                        child.gameObject.GetComponent<Renderer>().material.color = col;
                        //Debug.Log($"opacity = {opacity} -- material alpha: {child.gameObject.GetComponent<Renderer>().material.color.a}");
                    }
                }

                 //Se desvanece poco a poco hasta que se destruye
            }*/
        }
        else
        {

            Destroy(gameObject);
        }
        
    }

    public void Dead() //External Method
    {
        isAlive = false;
        //play animation
        Debug.Log("El enemigo ha muerto (en cuanto ocurra la animacion)");
        this.GetComponent<Animator>().SetBool("isDead", true);
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Attacking":
                    attackTime = clip.length;
                    break;
                case "Damage":
                    damageTime = clip.length;
                    break;
                case "SkeletonOutlaw@Dead01":
                    deathTime = clip.length;
                    //Debug.Log($"Se ha asignado la duracion de la animacion de muerte, duracion = {deathTime}");
                    break;
                case "SkeletonOutlaw@Idle00":
                    idleTime = clip.length;
                    //Debug.Log($"Se ha asignado la duracion de la animacion de Idle, duracion = {idleTime}");
                    break;
                default:
                    Debug.Log($"No se ha encontrado nada para esta animacion = {clip.name}");
                    break;
            }
        }
    }

    public void SelectedEnemy()
    {
        //rend.material.shader = shaderSelected;
        if (isAlive)
        {
            foreach (Transform child in this.transform)
            {
                if (child.gameObject.GetComponent<Renderer>() != null)
                {
                    //child.gameObject.GetComponent<Renderer>().material.shader = shaderSelected;
                    child.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    isSelected = true;
                }
            }

            if (renderSelectable != null)
            {
                renderSelectable.material.color = Color.red;
                isSelected = true;
            }
            

        }
    }
    public void UnselectedEnemy()
    {
        //rend.material.shader = shaderUnselected;

        foreach (Transform child in this.transform)
        {
            if (child.gameObject.GetComponent<Renderer>() != null)
            {
                child.gameObject.GetComponent<Renderer>().material.color = Color.white;
                isSelected = false;
                //child.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            if (renderSelectable != null)
            {
                renderSelectable.material.color = Color.white;
                isSelected = false;
            }

        }
    }

}

