using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent nav;

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player defeated");
            //player.GetComponent<Explode>().Main();
            player.GetComponent<PlayerController>().health = -1;
            //Destroy(collision.collider.gameObject);
        }
    }
}
