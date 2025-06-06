using UnityEngine;

public enum GoalType
{
    Kill = 0,
    Gather = 1,
}

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return currentAmount >= requiredAmount;
    }

    public void EnemyKilled()
    {
        if (goalType == GoalType.Kill)
        {
            currentAmount++;
        }  
    }

    public void ItemCollected()
    {
        if (goalType == GoalType.Gather)
        {
            currentAmount++;
        }
    }
}
