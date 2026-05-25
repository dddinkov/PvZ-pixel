using UnityEngine;
using System;

public abstract class ResourceManager : MonoBehaviour
{
    public event Action<int> OnAmountChanged;

    [SerializeField]
    protected int startAmount = 0;
    [SerializeField]
    protected int maxAmount = 10000;

    public int Amount {get; protected set;}

    protected virtual void Awake()
    {
        Amount = startAmount;
    }

    public virtual void Add(int amount)
    {
        Amount = Mathf.Min(Amount + amount, maxAmount);

        OnAmountChanged?.Invoke(Amount);
    }

    public virtual bool CanSpend(int amount)
    {
        return Amount >= amount;
    }

    public virtual bool TrySpend(int amount)
    {
        if(!CanSpend(amount))
        {
            return false;
        }

        Amount -= amount;

        OnAmountChanged?.Invoke(Amount);

        return true;
    }
}
