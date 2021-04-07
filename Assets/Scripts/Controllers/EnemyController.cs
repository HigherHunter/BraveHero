using UnityEngine;
using UnityEngine.AI;

//controls enemy behavior
public class EnemyController : MonoBehaviour
{
    //is obj active (not dead)
    public bool active = true;
    //look for player
    public float lookRadius = 10f;
    //random wandering in that radius
    public float wanderRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    // Use this for initialization
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            //distance from target
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                //get to target
                agent.SetDestination(target.position);
                //if got to target
                if (distance <= agent.stoppingDistance)
                {
                    //attack target
                    CharacterStats targetStats = target.GetComponent<CharacterStats>();
                    if (targetStats != null)
                    {
                        combat.Attack(targetStats);
                    }
                    //face target
                    FaceTarget();
                }
            }
            else
            {
                //if not wandering, start wandering
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        //set random position to go to
                        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
                        randomDirection += transform.position;
                        NavMeshHit hit;
                        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1))
                        {
                            agent.SetDestination(hit.position);
                        }
                    }
                }
            }
        }
    }
    //rotate towards target
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}
