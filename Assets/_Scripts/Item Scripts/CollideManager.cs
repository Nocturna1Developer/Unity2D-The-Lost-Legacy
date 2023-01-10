using UnityEngine;
using System.Collections;
using CodeMonkey.HealthSystemCM;
using UnityEngine.SceneManagement;
using FirstGearGames.SmoothCameraShaker;

public class CollideManager : MonoBehaviour, IGetHealthSystem 
{
    [Header("Core Properties")]
    [SerializeField] private int _coinValue = 30;
    [SerializeField] private int _maxScore = 150;
    [SerializeField] private float _fungusDamage = 15f;
    [SerializeField] private float _enemyDamage = 30f;
    [SerializeField] private float _healAmount = 15f;

    [SerializeField] private GameObject _playerBody;
    //[SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _faderImage;

    [Header("Camera Properties")]
    [SerializeField] private ShakeData _screenShakeData = null;
    [SerializeField] private ShakeData _jumpShakeData = null;
    private ShakerInstance             _screenShakeInstance;

    [Header("Core Properties")]
    private HealthSystem healthSystem;

    [Header("Particle Properties")]
    [SerializeField] private ParticleSystem _damageParticleSystem;
    [SerializeField] private ParticleSystem _healParticleSystem;
    [SerializeField] private ParticleSystem _coinParticleSystem;

    [Header("Audio Properties")]
    [SerializeField] private AudioSource _coinSound;
    [SerializeField] private AudioSource _deathSound; 
    [SerializeField] private AudioSource _hurtSound;   
    [SerializeField] private AudioSource _healSound;  

    private void Awake()
    {
        healthSystem = new HealthSystem(100);

        healthSystem.OnDead += HealthSystem_OnDead;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    private void Start()
    {
        _faderImage.gameObject.SetActive(true);
        LeanTween.alpha(_faderImage, 1, 0);
        LeanTween.alpha(_faderImage, 0, 2f).setOnComplete(() => {
            _faderImage.gameObject.SetActive(false);
        });
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e) 
    {
        _deathSound.Play();
        _playerBody.SetActive(false);
        //_playerController.enabled = false;
        StartCoroutine(RestartLevelAfterDelay(0.3f));
        _damageParticleSystem.Play();

        _screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        _damageParticleSystem.Play();
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e) {
        _healParticleSystem.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(SumScore.Score > _maxScore)
        {
            StartCoroutine(LoadNextlevelAfterDelay(0.3f));
        }

        switch(other.gameObject.tag)
        {
            case "Coin":
                SumScore.Add(_coinValue);
                _coinParticleSystem.Play();
                _coinSound.Play();
                break;

            case "Enemy":
                _hurtSound.Play();
                healthSystem.Damage(_enemyDamage);
                _damageParticleSystem.Play();
                _screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
                break;
            
            case "Fungus":
                _hurtSound.Play();
                healthSystem.Damage(15f);
                _damageParticleSystem.Play();
                _screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
                break;
            
            case "Heal":
                _healSound.Play();
                healthSystem.Heal(_healAmount);
                _healParticleSystem.Play();
                //_screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
                break;
            
            case "Death":
                _playerBody.SetActive(false);
                StartCoroutine(RestartLevelAfterDelay(0.3f));
                break;
        }
    }

    private IEnumerator RestartLevelAfterDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator LoadNextlevelAfterDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

