using UnityEngine;
using System.Collections;
using CodeMonkey.HealthSystemCM;

public class EnemyAI : MonoBehaviour, IGetHealthSystem 
{
    [Header("Core Properties")]
    [SerializeField] private float _playerDamage = 30f;
    [SerializeField] private GameObject _enemyBody;

    [Header("Core Properties")]
    private HealthSystem healthSystem;

    [Header("Particle Properties")]
    [SerializeField] private ParticleSystem _damageParticleSystem;
    [SerializeField] private ParticleSystem _deathParticleSystem;

    [Header("Audio Properties")]
    [SerializeField] private AudioSource _deathSound; 
    [SerializeField] private AudioSource _hurtSound;  

    [Header("Animator Properties")]
    public Animator _animator; 

    private void Awake()
    {
        healthSystem = new HealthSystem(50);

        healthSystem.OnDead += HealthSystem_OnDead;
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e) 
    {
        _deathParticleSystem.Play();
        _deathSound.Play();
        _enemyBody.SetActive(false);
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "PlayerProjectile":
                _hurtSound.Play();
                _animator.SetTrigger("GetHit");
                healthSystem.Damage(_playerDamage);
                _damageParticleSystem.Play();
                break;
        }
    }
}

