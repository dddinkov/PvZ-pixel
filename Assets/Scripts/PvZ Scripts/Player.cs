using UnityEngine;
public class Player : MonoBehaviour
{
    public const int MAX_NUMBER_OF_PLANTS = 40;
    private int level = 1;
    private bool[] unlockedPlants = new bool[MAX_NUMBER_OF_PLANTS];
    private void Start()
    {
        unlockedPlants[0] = true;
        LoadPlayer();
    }

    public int GetLevel()
    {
        return level;
    }
    public bool[] GetUnlockedPlants()
    {
        bool[] copy = new bool[MAX_NUMBER_OF_PLANTS];
        for(int i = 0; i < MAX_NUMBER_OF_PLANTS; ++i)
        {
            copy[i] = unlockedPlants[i];
        }
        return copy;
    }

    public void IncreaseLevel()
    {
        level++;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null)
        {
            FromPlayerData(data);
        }
        else
        {
            Debug.Log("Missing player data! Initializing empty file.");
        }
    }
    public void FromPlayerData(PlayerData data)
    {
        level = data.GetLevel();
        this.unlockedPlants = data.GetUnlockedPlants();
    }

    public bool IsPlantUnlocked(int index)
    {
        return unlockedPlants[index];
    }
    public void UnlockPlant(int index)
    {
        if (0 > index || index >= MAX_NUMBER_OF_PLANTS)
        {
            Debug.Log("Index out of bounds: " + index);
        }
        else
        {
            unlockedPlants[index] = true;
            SaveSystem.SavePlayer(this);
        }
    }

    public void ResetPlayerData()
    {
        SaveSystem.SavePlayer(new Player());
        SaveSystem.LoadPlayer();
    }
}
