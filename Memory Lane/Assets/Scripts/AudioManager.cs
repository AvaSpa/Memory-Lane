using Assets.Scripts.Utils;
using System.Collections;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (var sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.loop = sound.Loop;
        }
    }

    private void Start()
    {
        StartCoroutine(CyclePlayAll());
    }

    private IEnumerator CyclePlayAll()
    {
        while (true)
        {
            foreach (var sound in Sounds)
            {
                sound.Source.Play();
                while (sound.Source.isPlaying)
                {
                    yield return null;
                }
            }
        }
    }

    public void Play(string name)
    {
        var sound = Sounds.FirstOrDefault(s => s.Name == name);
        sound.Source.Play();
    }
}
