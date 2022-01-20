using UnityEngine;
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume=.7f;
    [Range(-3f, 3f)]
    public float pitch=1f;
    [Range(0f, .5f)]
    public float RandomVolume=.1f;
    [Range(0f, .5f)]
    public float RandomPitch=.1f;
    private AudioSource source;
    public bool Looping = false;
    public void SetSource(AudioSource _audioSource)
    {
        source = _audioSource;
        source.clip = clip;
        source.loop = Looping;
    }
    public void Play()
    {
        source.volume = volume*(1+Random.Range(-RandomVolume/2,RandomVolume/2));
        source.pitch = pitch * (1 + Random.Range(-RandomPitch / 2, RandomPitch / 2));
        source.Play();
    }
    public void Stop()
    {
        source.Stop();
    }
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake()
    {
        if (instance!=null)
        {
            if (instance!=this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    public Sound[] sounds;
    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound Player of " + sounds[i].name);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
            _go.transform.SetParent(transform);
        }
        PlaySound("Music");
    }
    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name==_name)
            {
                sounds[i].Play();
                return;
            }
        }
        print("snd nt fnd");
    }
    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
            print("snd nt fnd");
        }
    }
}
