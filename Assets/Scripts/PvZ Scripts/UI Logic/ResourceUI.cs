using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [SerializeField]
    private ResourceManager resource;
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private int maxDisplayAmount = 9990;

    private void Start()
    {
        Initialize();
    }
    private void UpdateUI(int amount)
    {
        if(amount > maxDisplayAmount)
        {
            text.text = maxDisplayAmount.ToString();
        }
        else
        {
            text.text = amount.ToString();
        }
    }

    private void OnDestroy()
    {
        if(resource != null)
        {
            resource.OnAmountChanged -= UpdateUI;
        }
    }

    private void Initialize()
    {
        if(resource == null)
        {
            return;
        }

        resource.OnAmountChanged += UpdateUI;

        UpdateUI(resource.Amount);
    }
}