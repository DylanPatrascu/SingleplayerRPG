using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public QuestSO questData;
    public PlayerStats player;

    public GameObject questWindow;
    public TMP_Text questTitle;
    public TMP_Text questDescription;
    public TMP_Text questExp;
    public TMP_Text questGold;
    public Button acceptButton;

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        questTitle.text = questData.title;
        questDescription.text = questData.description;
        questExp.text = questData.expReward.ToString();
        questGold.text = questData.goldReward.ToString();
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(AcceptQuest);
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.AddQuest(questData); // Automatically creates a runtime copy
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }
}
