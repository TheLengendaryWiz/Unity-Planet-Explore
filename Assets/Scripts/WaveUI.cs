using UnityEngine.UI;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public WaveSpawner spawner;
    public Animator waveAnimator;
    public Text waveCountdownText;
    public Text waveNoText;
    WaveSpawner.Spawnstate previousState;
    void Update()
    {
        switch (spawner.State)
        {
            case WaveSpawner.Spawnstate.spawning:
                UpdateSpawningUI();
                break;
            case WaveSpawner.Spawnstate.counting:
                UpdateCountingUI();
                break;
        }
        previousState = spawner.State;
    }
    void UpdateCountingUI()
    {
        if (previousState!=WaveSpawner.Spawnstate.counting)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown",true);
        }
        waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
    }
    void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.Spawnstate.spawning)
        {
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveIncoming", true);
            waveNoText.text = spawner.NextWave.ToString();
        }
    }
}