using UnityEngine;
using UnityEngine.AI;

//controls animations and animation triggers
public class CharacterAnimator : MonoBehaviour
{
    //is obj active (not dead)
    public bool isActive;
    public AnimationClip replacableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    const float animationSmoothTime = .1f;

    protected Animator animator;
    NavMeshAgent agent;
    protected CharacterCombat combat;
    protected AnimatorOverrideController overrideController;

    // Use this for initialization
    protected virtual void Start()
    {
        isActive = true;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        //for enemy current animation is always default, no swaping
        currentAttackAnimSet = defaultAttackAnimSet;
        //register function trigger
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isActive)
        {
            //add speed and animate
            float speed = agent.velocity.magnitude / agent.speed;
            animator.SetFloat("speed", speed, animationSmoothTime, Time.deltaTime);
            //check if in combat and set animation trigger
            animator.SetBool("inCombat", combat.inCombat);
        }
    }

    protected virtual void OnAttack()
    {
        //set attack trigger and pick random attack animation
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replacableAttackAnim] = currentAttackAnimSet[attackIndex];
    }
}
