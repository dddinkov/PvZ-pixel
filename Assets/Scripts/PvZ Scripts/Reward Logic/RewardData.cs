using UnityEngine;

[CreateAssetMenu(fileName = "RewardData", menuName = "Game/RewardData", order = 1)]
public class RewardData : ScriptableObject
{
    public RewardType rewardType;
    public Sprite icon;
    public int key;
}