using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float Health;
    [SerializeField] float MaxHealth;
    [SerializeField] float Level;
    [SerializeField] float ExpGranted;
    [SerializeField] bool IsDead = false;

    private void Start()
    {
        MaxHealth = Health;
    }
    public void Heal(float hp)
    {
        Health += hp;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        IsDead = true;
        Debug.Log("Enemy DEAD");
        Object.Destroy(this.gameObject);
    }
}
