using UnityEngine;

/// <summary>
/// Script to handle sound of the game
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource fxSource;
    public AudioClip gameOverSound, gameCompleteSound, ballHitSound;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlaySound(FxTypes fxTypes)
    {
        switch (fxTypes)
        {
            case FxTypes.GAME_OVER:
                fxSource.PlayOneShot(gameOverSound);
                break;
            case FxTypes.GAME_COMPLETE:
                fxSource.PlayOneShot(gameCompleteSound);
                break;
            case FxTypes.BALL_HIT:
                fxSource.PlayOneShot(ballHitSound);
                break;
        }

    }
}


public enum FxTypes
{
    GAME_OVER,
    GAME_COMPLETE,
    BALL_HIT
}
