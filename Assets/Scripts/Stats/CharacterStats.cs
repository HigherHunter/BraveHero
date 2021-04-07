using UnityEngine;

//base stats class for every character
public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; protected set; }

    public Stat damage;
    public Stat armor;

    //triggers
    public event System.Action<int, int> OnHealthChanged;
    public event System.Action OnDeathSoundTrigger;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    protected void Update()
    {
        //stop healing
        if (currentHealth == maxHealth)
        {
            CancelInvoke();
        }
    }

    //takes damage from opponent
    public void TakeDamage(int damage)
    {
        damage -= armor.GetBaseValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        CancelInvoke();
        //heals over time
        InvokeRepeating("HealCharacter", 5, 1);
    }

    public virtual void Die()
    {
        // die somehow
        OnDeathSoundTrigger();
    }

    //heals character
    private void HealCharacter()
    {
        currentHealth++;
        OnHealthChanged(maxHealth, currentHealth);
    }

}
