using UnityEngine;
using System.Collections;
using CodeMonkey.HealthSystemCM;

public class FungusAI : MonoBehaviour, IGetHealthSystem 
{
    [Header("Core Properties")]
    [SerializeField] private float _playerDamage = 30f;
    [SerializeField] private GameObject _enemyBody;

    [Header("Core Properties")]
    private HealthSystem healthSystem;

    [Header("Particle Properties")]
    [SerializeField] private ParticleSystem _damageParticleSystem;

    [Header("Audio Properties")]
    [SerializeField] private AudioSource _deathSound; 

    private void Awake()
    {
        healthSystem = new HealthSystem(5);

        healthSystem.OnDead += HealthSystem_OnDead;
        //healthSystem.OnDamaged += HealthSystem_OnDamaged;
        //healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e) 
    {
        _deathSound.Play();
        _enemyBody.SetActive(false);
        Destroy(gameObject);
        _damageParticleSystem.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "PlayerProjectile":
                healthSystem.Damage(_playerDamage);
                _damageParticleSystem.Play();
                break;
        }
    }
}

