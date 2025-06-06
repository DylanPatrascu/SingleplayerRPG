using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerStats player;
    public GameObject questWindow;
    public TMP_Text questTitle;
    public TMP_Text questDescription;
    public TMP_Text questExp;
    public TMP_Text questGold;

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        questTitle.text = quest.title;
        questDescription.text = quest.description;
        questExp.text = quest.expReward.ToString();
        questGold.text = quest.goldReward.ToString();
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        quest.isActive = true;
        player.AddQuest(quest);
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }

}
