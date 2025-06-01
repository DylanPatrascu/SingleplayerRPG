using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Health { get; private set; }
    public float MaxHealth { get; private set; }
    public float Level { get; private set; }
    public float MaxLevel { get; private set; }
    public float Exp { get; private set; }
    public float MaxExp { get; private set; }
    public float Stamina { get; private set; }
    public bool IsDead { get; private set; } = false;

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

    public void GainExp(float xp)
    {
        Exp += xp;
        if (Exp >= MaxExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        Level++;
        if (Level > MaxLevel)
        {
            Level = MaxLevel;
        }
    }

    public void Die()
    {
        IsDead = true;
        Debug.Log("DEAD");
    }
}
