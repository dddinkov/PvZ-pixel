using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Text levelText;
    private Player player;
    private SceneLoader sceneLoader;
    const string playSceneName = "PvZ Scene";
    public void LoadPlayScene()
    {
        sceneLoader.LoadScene(1);
    }
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        sceneLoader = GameObject.Find("Scene Loader").GetComponent<SceneLoader>();
    }

    void Update()
    {
        UpdateLevelText();
    }

    void UpdateLevelText()
    {
        int level = player.GetLevel();
        int stage = CalculateLevelStage(level);
        int actualLevel = CalculateActualLevel(level);
        levelText.text = stage + "-" + actualLevel;
    }
    int CalculateLevelStage(int level)
    {
        int stage = (level - 1) / 10 + 1;
        return stage;
    }
    int CalculateActualLevel(int level)
    {
        if(level % 10 == 0)
        {
            return 10;
        }

        return level % 10;
    }
}
