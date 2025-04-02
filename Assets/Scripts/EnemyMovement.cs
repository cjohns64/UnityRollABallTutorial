using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    private Vector3 player_last_position;
    private bool last_destination_is_valid = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player_last_position = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {   
            if (last_destination_is_valid)
            {
                player_last_position = player.position;
            }
            last_destination_is_valid = navMeshAgent.SetDestination(player_last_position);
        }
        
    }
}
