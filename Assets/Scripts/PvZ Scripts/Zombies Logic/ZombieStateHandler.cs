using UnityEngine;

public class ZombieStateHandler : MonoBehaviour
{
    public ZombieState CurrentState { get; private set; } = ZombieState.Normal;

    public ParticleSystem poisonParticles;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Color poisonedColor = Color.green;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        poisonedColor = poisonParticles.main.startColor.color;
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
        poisonParticles?.Stop();
        spriteRenderer.color = Color.white;
    }
}