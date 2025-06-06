using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
public class QuestSO : ScriptableObject
{
    public string title;
    public string description;
    public int expReward;
    public int goldReward;
    public GoalType goalType;
    public int requiredAmount;
}
