using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] Transform sourceTransform;
    Transform selectedEnemy;
    [SerializeField] GameManager gameManager;


    void Start()
    {
        
    }

    void Update()
    {
        if (selectedEnemy != null)
        {
            selectedEnemy?.gameObject.GetComponent<SimpleEnemyBehaviour>().UnselectedEnemy();
        }

        ForwardRay();
    }

    public void ForwardRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(sourceTransform.position, sourceTransform.forward, out hit, LayerMask.GetMask("Enemies")))
        {
            Debug.DrawRay(sourceTransform.position, sourceTransform.forward * 20, Color.red);
            //Debug.Log($"Ha chocado con algo: {hit.collider.tag}");

            if (hit.collider.CompareTag("Enemy"))
            {
                if (gameManager.GetComponent<DragonController>().isOnDragon)
                {
                    selectedEnemy = hit.collider.gameObject.transform;
                    hit.collider.gameObject.GetComponent<SimpleEnemyBehaviour>().SelectedEnemy();
                }
            }
            else if (hit.collider.CompareTag("Dragon"))
            {
                /*
                selectedEnemy = hit.collider.gameObject.transform;
                //Montarse en el dragon
                gameManager.SetPlayerOnDragon();
                */
            }
        }
    }
}
