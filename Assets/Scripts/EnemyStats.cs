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
            QuestInstance quest = attacker.GetQuest();

            if (quest != null && quest.isActive && quest.questData.goalType == GoalType.Kill)
            {
                quest.AddProgress();
                if (quest.IsComplete())
                {
                    attacker.GainExp(quest.questData.expReward);
                    attacker.GainGold(quest.questData.goldReward);
                    quest.Complete();
                }
            }

        }
        Object.Destroy(this.gameObject);
    }
}
