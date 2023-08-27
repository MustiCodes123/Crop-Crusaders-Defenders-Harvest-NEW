using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReturnTiger : MonoBehaviour
{

    public void ReturnTigerFunction() { 
       

            Debug.Log("NOW RETURNING");

            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.SetDestination(GameObject.FindGameObjectWithTag("ReturnTag").transform.position);
            }

         }


    

}
