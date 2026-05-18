using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SunManager : MonoBehaviour
{
    public GameObject sunPrefab;
    public Transform leftPoint, rightPoint, upPoint, downPoint;
    private float time = 0.0f;


    public static TMP_Text sunAmountText;
    private static float totalSun = 50.0f;


    // Start is called before the first frame update
    void Start()
    {
        sunAmountText = GameObject.Find("Sun Amount Text").GetComponent<TMP_Text>();
        sunAmountText.text = totalSun.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        if(time >= 5.0f)
        {
            float r1, r2;
            r1 = Random.Range(0.0f, 1.0f);
            r2 = Random.Range(0.0f, 1.0f);
            float x, y;
            x = leftPoint.position.x - (leftPoint.position.x - rightPoint.position.x) * r1;
            y = upPoint.position.y - (upPoint.position.y - downPoint.position.y) * r2;
            Vector2 spawnPoint = new Vector2(x, y);
            Instantiate(sunPrefab, spawnPoint, Quaternion.identity);
            time = 0.0f;
        }
    }

    public static void AddSun(float amount)
    {
        if(totalSun >= 10000.0f)
        {
            return;
        }

        totalSun += amount;

        if (totalSun >= 10000.0f)
        {
            totalSun = 10000.0f;
            sunAmountText.text = "9990";
        }
        else
        {
            sunAmountText.text = totalSun.ToString();
        }
    }

    public static bool CanTakeSun(float amount)
    {
        return totalSun - amount >= 0.0f;
    }

    public static bool TakeSun(float amount)
    {
        totalSun -= amount;

        sunAmountText.text = totalSun.ToString();

        return true;
    }

}
