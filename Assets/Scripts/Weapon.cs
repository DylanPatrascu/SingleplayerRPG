using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerStats PlayerStats;
    [SerializeField] private PlayerState _playerState;

    public float Damage = 30f;
    public float CritChance = 25f;
    public float CritDamage = 1.25f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") && _playerState.CurrentPlayerCombatState == PlayerCombatState.Attacking)
        {
            EnemyStats enemyStats = other.gameObject.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(DealDamage());
        }
    }

    public float DealDamage()
    {
        float dmg = Damage + IsCrit();
        Debug.Log(dmg);
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
