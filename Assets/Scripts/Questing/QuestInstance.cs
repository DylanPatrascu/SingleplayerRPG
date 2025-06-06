using UnityEngine;

[System.Serializable]
public class QuestInstance
{
    public QuestSO questData;
    public bool isActive = true;
    public int currentAmount = 0;

    public QuestInstance(QuestSO data)
    {
        questData = data;
    }

    public bool IsComplete()
    {
        return currentAmount >= questData.requiredAmount;
    }

    public void AddProgress()
    {
        currentAmount++;
    }

    public void Complete()
    {
        isActive = false;
        Debug.Log(questData.title + " was completed");
    }
}

