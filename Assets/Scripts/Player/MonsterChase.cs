using UnityEngine;
using UnityEngine.AI;
public class MonsterChase : MonoBehaviour
{
    public static MonsterChase Instance;
    public Transform player;
    private NavMeshAgent agent;
    private bool chasing = false;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }
    void Update()
    {
        if (!chasing)
            return;
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
    public void StartChase()
    {
        chasing = true;
        agent.isStopped = false;
    }
    public void StopChase()
    {
        chasing = false;
        agent.isStopped = true;
    }
}