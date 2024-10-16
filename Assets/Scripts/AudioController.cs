using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private List<AudioSource> _soundEffects;
    [SerializeField] private List<AudioClip> _backgroundMusics;
    [SerializeField] private List<AudioClip> _zombieGroans;
    [SerializeField] private AudioClip _zombieWalk;
    [SerializeField] private List<AudioClip> _squashAlerts;
    [SerializeField] private List<AudioClip> _squashFly;
    [SerializeField] private List<AudioClip> _peaFires;
    [SerializeField] private List<AudioClip> _peaHits;

    private int _bmIndex;
    private int _previousBMIndex;
    private bool _isGroaning = false;
    private bool _isWalking = false;

    private void Start()
    {
        ZombieLocomotion.Walk.AddListener(ZombieWalk);
        UIManager.Alert.AddListener(SquashAlert);
        SquashLocomotion.Fly.AddListener(SquashFly);
        PeaFire.FirePea.AddListener(FirePea);
        PeaHit.HitPea.AddListener(HitPea);
    }

    private void Update()
    {
        BackgroundMusic();
        ZombieGroan();
    }

    private void BackgroundMusic()
    {
        if (_backgroundMusic.clip == null || _backgroundMusic.time >= _backgroundMusic.clip.length)
        {
            _bmIndex = Random.Range(0, _backgroundMusics.Count);

            if (_bmIndex != _previousBMIndex)
            {
                _backgroundMusic.clip = _backgroundMusics[_bmIndex];
                _backgroundMusic.Play();
                _previousBMIndex = _bmIndex;
            }
        }
    }

    private void ZombieGroan()
    {
        if (!_isGroaning)
        {
            StartCoroutine(IZombieGoran());
        }
    }

    private IEnumerator IZombieGoran()
    {
        _isGroaning = true;
        yield return new WaitForSeconds(Random.Range(5f, 7.5f));
        AudioSource soundEffect = _soundEffects.Find((value) => value.name == "Zombie Groan");
        soundEffect.clip = _zombieGroans[Random.Range(0, _zombieGroans.Count)];
        soundEffect.Play();
        _isGroaning = false;
    }

    private void ZombieWalk(bool status)
    {
        AudioSource soundEffect = _soundEffects.Find((value) => value.name == "Zombie Walk");

        if (status && !_isWalking)
        {
            StartCoroutine(IZombieWalk(soundEffect));
        }
        else if (!status && _isWalking)
        {
            soundEffect.Stop();
        }
    }

    private IEnumerator IZombieWalk(AudioSource soundEffect)
    {
        _isWalking = true;
        soundEffect.clip = _zombieWalk;
        soundEffect.Play();
        yield return new WaitForSeconds(0.75f);
        _isWalking = false;
    }

    private void SquashAlert()
    {
        AudioSource soundEffect = _soundEffects.Find((value) => value.name == "Squash Alert");
        soundEffect.clip = _squashAlerts[Random.Range(0, _squashAlerts.Count)];
        soundEffect.Play();
    }

    private void SquashFly(string status)
    {
        AudioSource soundEffect = _soundEffects.Find((value) => value.name == "Squash Fly");

        if (status == "Flying")
        {
            soundEffect.clip = ReverseClip(_squashFly.Find((value) => value.name == "blover"));
        }
        else if (status == "Falling")
        {
            soundEffect.clip = _squashFly.Find((value) => value.name == "blover");
        }
        else if (status == "Landing")
        {
            soundEffect.clip = _squashFly.Find((value) => value.name == "gargantuar_thump");
        }

        soundEffect.Play();
    }

    private void FirePea()
    {
        AudioSource soundEffect = _soundEffects.Find((value) => value.name == "Pea Fire");
        soundEffect.clip = _peaFires[Random.Range(0, _peaFires.Count)];
        soundEffect.Play();
    }

    private void HitPea(string status)
    {
        AudioSource soundEffect = _soundEffects.Find((value) => value.name == "Pea Hit");
        List<AudioClip> _regularPeaHits = _peaHits.FindAll((value) => value.name.Contains("splat"));
        AudioClip _frozenPeaHit = _peaHits.Find((value) => value.name == "frozen");

        soundEffect.clip = _regularPeaHits[Random.Range(0, _regularPeaHits.Count)];
        soundEffect.Play();

        if (status.Contains("Snow"))
        {
            soundEffect.clip = _frozenPeaHit;
            soundEffect.Play();
        }
    }

    private AudioClip ReverseClip(AudioClip clip)
    {
        // Get the audio clip's data
        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        // Reverse the audio samples
        System.Array.Reverse(samples);

        // Create a new audio clip and set the reversed data
        AudioClip reversedClip = AudioClip.Create(clip.name + "_Reversed",
                                                  clip.samples,
                                                  clip.channels,
                                                  clip.frequency,
                                                  false);
        reversedClip.SetData(samples, 0);
        return reversedClip;
    }
}
