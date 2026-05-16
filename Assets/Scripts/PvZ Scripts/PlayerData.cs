[System.Serializable]
public class PlayerData 
{
    private const int MAX_NUMBER_OF_PLANTS = Player.MAX_NUMBER_OF_PLANTS;
    private int level;
    private bool[] unlockedPlants = new bool[MAX_NUMBER_OF_PLANTS];
    public PlayerData(Player player)
    {
        if (player != null)
        {
            level = player.GetLevel();
            unlockedPlants = player.GetUnlockedPlants();
        }
    }
    public int GetLevel()
    {
        return level;
    }
    public bool[] GetUnlockedPlants()
    {
        bool[] copy = new bool[MAX_NUMBER_OF_PLANTS];
        for (int i = 0; i < MAX_NUMBER_OF_PLANTS; ++i)
        {
            copy[i] = unlockedPlants[i];
        }
        return copy;
    }
}
