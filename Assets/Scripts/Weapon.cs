using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerStats PlayerStats;
    [SerializeField] private PlayerState _playerState;

    public float Damage = 30f;
    public float CritChance = 25f;
    public float CritDamage = 1.25f;
    public bool DamageDealt = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && _playerState.CurrentPlayerCombatState == PlayerCombatState.Attacking && !DamageDealt)
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            PlayerStats playerStats = GetComponentInParent<PlayerStats>();

            float damage = DealDamage();
            enemyStats.TakeDamage(damage, playerStats);

            if (enemyStats.GetHealth() <= 0 && !enemyStats.GetDead())
            {
                enemyStats.Die(playerStats);
            }
            DamageDealt = true;
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
        else
        {
            return 0;
        }
    }
}
