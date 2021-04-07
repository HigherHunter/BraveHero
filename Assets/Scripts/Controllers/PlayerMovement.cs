using UnityEngine;
using UnityEngine.AI;

//manager player movement
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{

    Transform target;
    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target != null)
        {
            MoveToPoint(target.position);
            FaceTarget();
        }
    }
    //moves to target
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
    //followes given target
    public void FollowTarger(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;

        target = newTarget.interactionTransform;
    }
    //stops following
    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null;
    }
    //rotates towards target
    void FaceTarget()
    {
        // find direction towards target
        Vector3 direction = (target.position - transform.position).normalized;
        // how to rotate, no y (up)
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        // smooth rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
