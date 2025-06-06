using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerStats PlayerStats;
    [SerializeField] private PlayerState _playerState;

    public float Damage = 30f;
    public float CritChance = 25f;
    public float CritDamage = 1.25f;

    private HashSet<EnemyStats> _enemiesHit = new HashSet<EnemyStats>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && _playerState.CurrentPlayerCombatState == PlayerCombatState.Attacking)
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats == null || _enemiesHit.Contains(enemyStats)) return;

            PlayerStats playerStats = GetComponentInParent<PlayerStats>();
            float damage = DealDamage();

            enemyStats.TakeDamage(damage, playerStats);

            if (enemyStats.GetHealth() <= 0 && !enemyStats.GetDead())
            {
                enemyStats.Die(playerStats);
            }

            _enemiesHit.Add(enemyStats);
        }
    }

    public float DealDamage()
    {
        float dmg = Damage + IsCrit();
        return dmg;
    }

    public float IsCrit()
    {
        int odds = Random.Range(0, 100);
        if (odds <= CritChance)
        {
            return CritDamage * Damage;
        }
        return 0;
    }

    public void ResetEnemiesHit()
    {
        _enemiesHit.Clear();
    }
}
