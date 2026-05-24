using UnityEngine;

public class ZombieStateHandler : MonoBehaviour
{
    public ZombieState CurrentState { get; private set; } = ZombieState.Normal;

    public ParticleSystem poisonParticles;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Color poisonedColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        poisonParticles?.Stop();
    }

    public void SetState(ZombieState newState)
    {
        if (CurrentState == newState)
            return;

        StopEffects();

        CurrentState = newState;

        switch (newState)
        {
            case ZombieState.Poisoned:
                poisonParticles?.Play();
                spriteRenderer.color = poisonedColor;
                break;
            case ZombieState.Normal:
                spriteRenderer.color = Color.white;
                break;
            default:
                break;
        }
    }

    private void StopEffects()
    {
        if(this == null) return; // Check if the object is destroyed before trying to stop effects
        poisonParticles?.Stop();
        spriteRenderer.color = Color.white;
    }
}