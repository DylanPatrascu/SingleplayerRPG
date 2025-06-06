using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] private bool canInteract;
    private PlayerState _playerState;
    private PlayerStats _player;

    private void Update()
    {
        if (canInteract && _playerState.CurrentPlayerCombatState == PlayerCombatState.Gathering)
        {
            Gather(_player);
        }
    }
    public void Gather(PlayerStats player)
    {
        Debug.Log("Item Gathered");
        if (player != null)
        {
            //add to inventory here

            QuestInstance q = player.GetQuest();
            if (q != null && q.isActive && q.questData.goalType == GoalType.Gather)
            {
                q.AddProgress();
                if (q.IsComplete())
                {
                    player.GainExp(q.questData.expReward);
                    player.GainGold(q.questData.goldReward);
                    q.Complete();
                }
            }
        }
        Object.Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject.GetComponent<PlayerStats>();
            _playerState = other.gameObject.GetComponent<PlayerState>();
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = null;
            _playerState = null;
            canInteract = false;
        }
    }
}
