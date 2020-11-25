using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] Transform sourceTransform;

    Transform selectedEnemy;
    void Start()
    {
        
    }

    void Update()
    {
        if (selectedEnemy != null)
        {
            selectedEnemy.gameObject.GetComponent<SimpleEnemyBehaviour>().UnselectedEnemy();
        }

        ForwardRay();
    }

    public void ForwardRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(sourceTransform.position, sourceTransform.forward, out hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                selectedEnemy = hit.collider.gameObject.transform;
                hit.collider.gameObject.GetComponent<SimpleEnemyBehaviour>().SelectedEnemy();

            }
        }
    }
}
