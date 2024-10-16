using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private List<AudioSource> _soundEffects;
    [SerializeField] private List<AudioClip> _backgroundMusics;
    [SerializeField] private List<AudioClip> _zombieGroans;
    private int _bmIndex;
    private int _previousBMIndex;
    private bool _isGroaning = false;

    private void Update()
    {
        BackgroundMusic();
        ZombieGroan();
    }

    private void BackgroundMusic()
    {
        if (_backgroundMusic.clip == null || _backgroundMusic!.isPlaying == false)
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
        //foreach (AudioSource soundEffect in _soundEffects)
        //{
        //    if (soundEffect.name == "Zombie Groan")
        //    {
        //        soundEffect.clip = _zombieGroans[Random.Range(0, _zombieGroans.Count)];
        //        soundEffect.Play();
        //        break;
        //    }
        //}
        _isGroaning = false;
    }

}
