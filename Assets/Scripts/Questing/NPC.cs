using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private bool canInteract;
    [SerializeField] private QuestGiver questGiver;
    [SerializeField] private PlayerState _playerState;


    private void Awake()
    {
        questGiver = GetComponent<QuestGiver>();
    }
    private void Update()
    {
        if (canInteract && _playerState.CurrentPlayerCombatState == PlayerCombatState.Gathering)
        {
            questGiver.OpenQuestWindow();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            questGiver.CloseQuestWindow();
        }
    }
}
