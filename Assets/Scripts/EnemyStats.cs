using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float Health;
    [SerializeField] private float MaxHealth;
    [SerializeField] private float Level;
    [SerializeField] private float ExpGranted;
    [SerializeField] private float GoldGranted;

    [SerializeField] private bool IsDead = false;

    private void Start()
    {
        MaxHealth = Health;
    }

    public float GetHealth()
    {
        return Health;
    }

    public bool GetDead()
    {
        return IsDead;
    }

    public void Heal(float hp)
    {
        Health += hp;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public void TakeDamage(float dmg, PlayerStats attacker)
    {
        Health -= dmg;
        if (Health <= 0 && !IsDead)
        {
            Die(attacker);
        }
    }


    public void Die(PlayerStats attacker)
    {
        IsDead = true;
        Debug.Log("Enemy DEAD");
        if (attacker != null)
        {
            attacker.GainExp(ExpGranted);
            attacker.GainGold(GoldGranted);
            Quest q = attacker.GetComponentInParent<PlayerStats>().GetQuest();

            if(q.isActive && q.goal.goalType == GoalType.Kill)
            {
                q.goal.EnemyKilled();
                if(q.goal.IsReached())
                {
                    attacker.GainExp(q.expReward);
                    attacker.GainGold(q.goldReward);
                    q.Complete();
                }
            }
        }
        Object.Destroy(this.gameObject);
    }
}
