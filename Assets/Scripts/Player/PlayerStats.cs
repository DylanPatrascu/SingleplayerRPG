using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float Health;
    [SerializeField] private float MaxHealth;
    [SerializeField] private float Level;
    [SerializeField] private float MaxLevel;
    [SerializeField] private float Exp;
    [SerializeField] private float MaxExp;
    [SerializeField] private float Stamina;
    [SerializeField] private float Gold;
    [SerializeField] private bool IsDead = false;

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

    public void GainGold(float gold)
    {
        Gold += gold;
    }

    public void Die()
    {
        IsDead = true;
        Debug.Log("DEAD");
    }
}
