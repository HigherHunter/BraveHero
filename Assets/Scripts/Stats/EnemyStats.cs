using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//stats for all enemies
public class EnemyStats : CharacterStats
{

    Animator animator;
    EnemyController enemyController;
    Enemy enemy;
    public RagdollManager ragdoll;

    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        enemy = GetComponent<Enemy>();
    }

    //what happens when certain enemy dies
    public override void Die()
    {
        if (tag == "Boss")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            base.Die();
            if (tag == "SpiderQueen")
            {
                ObjectivesManager.instance.DrawText("Find the portal.");
            }
            // death animation
            if (ragdoll != null)
            {
                GetComponent<CharacterAnimator>().isActive = false;
                ragdoll.Setup();
            }
            else
            {
                animator.SetTrigger("dead");
            }

            StartCoroutine(DestroyObject(30));

            enemyController.active = false;
            enemy.radius = 0f;

            //loot
            LootManager lootManager = GetComponent<LootManager>();
            lootManager.DropLoot();
        }
    }

    //destroys dead object after certain time
    IEnumerator DestroyObject(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
