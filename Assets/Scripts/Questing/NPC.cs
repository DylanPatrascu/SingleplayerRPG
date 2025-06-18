using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private bool canInteract;
    [SerializeField] private QuestGiver questGiver;
    [SerializeField] private PlayerState _playerState;
    private Transform _player;
    public float rotationSpeed = 10f;




    private void Awake()
    {
        questGiver = GetComponent<QuestGiver>();
    }
    private void Update()
    {
        if (canInteract)
        {
            // 1. Get the direction from the object to the player
            Vector3 direction = (_player.position - transform.position).normalized;

            // 2. Zero out the Y component so we only rotate on the Y axis
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                // 3. Calculate the target rotation using Quaternion.LookRotation
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // 4. Smooth rotation using Quaternion.RotateTowards
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
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
            _player = other.gameObject.GetComponent<Transform>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            questGiver.CloseQuestWindow();
            _player = null;
        }
    }
}
