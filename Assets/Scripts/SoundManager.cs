using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioClip ambianceSpaceClip;
    public AudioClip bodyPartSound1Clip;
    public AudioClip bodyPartSound2Clip;
    public AudioClip explosionCrunchClip;
    public AudioClip jetFireClip;
    public AudioClip throwingMeteorClip;
    public AudioClip holdingMeteorClip;
    public AudioClip mainMenuMusicClip;
    public AudioClip breathingSoundClip;

    private bool playBodyPartSound1Next = true; // Track which body part sound to play next

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayAmbianceSpace();
    }

    private AudioSource CreateAudioSource(AudioClip clip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        return audioSource;
    }

    public void PlayAmbianceSpace()
    {
        AudioSource source = CreateAudioSource(ambianceSpaceClip);
        source.loop = true;
        source.Play();
    }

    public void PlayBodyPartSound()
    {
        AudioClip clip = playBodyPartSound1Next ? bodyPartSound1Clip : bodyPartSound2Clip;
        AudioSource source = CreateAudioSource(clip);
        source.PlayOneShot(clip);
        playBodyPartSound1Next = !playBodyPartSound1Next;
    }

    public void PlayExplosionCrunch()
    {
        AudioSource source = CreateAudioSource(explosionCrunchClip);
        source.PlayOneShot(explosionCrunchClip);
    }

    public void PlayJetFire()
    {
        AudioSource source = CreateAudioSource(jetFireClip);
        source.PlayOneShot(jetFireClip);
    }

    public void PlayThrowingMeteor()
    {
        AudioSource source = CreateAudioSource(throwingMeteorClip);
        source.PlayOneShot(throwingMeteorClip);
    }

    public void PlayHoldingMeteor()
    {
        AudioSource source = CreateAudioSource(holdingMeteorClip);
        source.PlayOneShot(holdingMeteorClip);
    }

    public void PlayMainMenuMusic()
    {
        AudioSource source = CreateAudioSource(mainMenuMusicClip);
        source.loop = true;
        source.Play();
    }

    public void StopMainMenuMusic()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (var source in sources)
        {
            if (source.clip == mainMenuMusicClip)
            {
                source.Stop();
                Destroy(source); // Clean up
                break;
            }
        }
    }

    public void PlayBreathingSound()
    {
        AudioSource source = CreateAudioSource(breathingSoundClip);
        source.PlayOneShot(breathingSoundClip);
    }
}

// For calling it "SoundManager.Instance.PlayBreathingSound();"