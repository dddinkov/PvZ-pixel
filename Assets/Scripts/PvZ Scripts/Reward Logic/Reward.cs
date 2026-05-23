using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    private RewardData rewardData;
    private RewardType rewardType;
    [SerializeField]
    private float flightDistance = 6.0f;
    private Vector2[] points;
    [SerializeField]
    private float duration = 1.2f;
    float t = 0;
    private Player player;
    private Image rewardIcon;

    void Start()
    {
        points = new Vector2[3];
        Transform transform = gameObject.transform;
        points[0] = transform.position;
        points[1] = new Vector2(transform.position.x, transform.position.y + Random.Range(flightDistance / 2, flightDistance));
        points[2] = new Vector2(transform.position.x - Random.Range(flightDistance / 2, flightDistance), transform.position.y - Random.Range(flightDistance/3, flightDistance * 2 / 3));
        player = GameObject.Find("Player").GetComponent<Player>();
        int rewardIndex = System.Math.Min(player.GetLevel(), Resources.LoadAll<RewardData>("Rewards").Length - 1);
        rewardData = Resources.Load<RewardData>("Rewards/RewardData_" + rewardIndex);
        rewardType = rewardData.rewardType;
        rewardIcon = GetComponent<Image>();
        rewardIcon.sprite = rewardData.icon;
        GetComponent<Button>().onClick.AddListener(OnRewardClick);
    }

    void FixedUpdate()
    {
        Interpolate();
    }

    private void Interpolate()
    {
        if (t <= 1.0f)
        {
            float x = (1 - t) * (1 - t) * points[0].x + 2 * (1 - t) * t * points[1].x + t * t * points[2].x;
            float y = (1 - t) * (1 - t) * points[0].y + 2 * (1 - t) * t * points[1].y + t * t * points[2].y;
            transform.position = new Vector3(x, y, 0);
            t += Time.fixedDeltaTime / duration;
        }
    }
    
    private void OnRewardClick()
    {
        int key = rewardData.key;
        switch (rewardType)
        {
            case RewardType.Plant:
                player.UnlockPlant(key);
                break;
            default:
                Debug.LogWarning("Undefined reward type: " + rewardType);
                break;
        }
        player.IncreaseLevel();
        player.SavePlayer();
        // Back to main menu for now
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
