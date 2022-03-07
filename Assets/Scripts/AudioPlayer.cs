using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioClip> sounds = new List<AudioClip>();

    public AudioSource player
    {
        get
        {
            return _player;
        }
        protected set
        {
            _player = value;
        }
    }
    [SerializeField]
    protected AudioSource _player;

    public AudioClip sound
    {
        get
        {
            return _sound;
        }
        protected set
        {
            _sound = value;
        }
    }
    AudioClip _sound;


    public void SetSource(AudioSource source)
    {
        player = source;
    }

    public void PlaySoundHit(int i)
    {
        player.PlayOneShot(sounds[i]);
    }

    public void PlaySound(int i)
    {
        player.clip = sounds[i];
        player.Play();
    }

}
