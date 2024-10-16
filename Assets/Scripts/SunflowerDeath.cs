using UnityEngine;
using UnityEngine.Events;

public class SunflowerDeath : MonoBehaviour
{

    public static UnityEvent DeathState = new UnityEvent();
    private float _hp;
    private bool _dead;

    private void Start()
    {
        ZombieLocomotion.ZombieAttack.AddListener(Damage);
        _hp = 100f;
    }

    private void Update()
    {
        if (_hp <= 0f && !_dead)
        {
            DeathState.Invoke();
            _dead = true;
            gameObject.SetActive(false);
        }
    }

    public void ResetSunflower()
    {
        _hp = 100f;
        _dead = false;
        gameObject.SetActive(true);
    }

    private void Damage()
    {
        _hp -= 10f;
    }

}
