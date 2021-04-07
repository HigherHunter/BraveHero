using UnityEngine;

//controls combat behavior
[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{

    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    const float combatCooldown = 5f;
    float lastAttackTime;

    public float attackDelay = .6f;

    //is object in combat
    public bool inCombat { get; private set; }

    //triggeres
    public event System.Action OnAttack;
    public event System.Action<string> OnHitSoundTrigger;

    CharacterStats myStats;
    CharacterStats opponentStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            inCombat = false;
        }
    }

    //registers the attack
    public void Attack(CharacterStats targetStats)
    {
        opponentStats = targetStats;
        if (attackCooldown <= 0f)
        {
            if (OnAttack != null)
            {
                OnAttack();
            }

            attackCooldown = 1f / attackSpeed;
            inCombat = true;
            lastAttackTime = Time.time;
        }
    }

    //when triggered does the attack
    public void AttackHitAnimation()
    {
        if (OnHitSoundTrigger != null)
        {
            OnHitSoundTrigger(opponentStats.gameObject.tag);
        }
        opponentStats.TakeDamage(myStats.damage.GetBaseValue());
        if (opponentStats.currentHealth <= 0)
        {
            inCombat = false;
        }
    }
}
