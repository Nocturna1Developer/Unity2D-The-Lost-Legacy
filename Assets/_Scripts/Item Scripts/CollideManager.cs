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
    [SerializeField] private RectTransform _collectImage;
    [SerializeField] private RectTransform _damageImage;
    [SerializeField] private RectTransform _healImage;

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
        //healthSystem.OnDamaged += HealthSystem_OnDamaged;
        //healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    private void Start()
    {
        // Screen flashes when interacting with objects in scene
        _faderImage.gameObject.SetActive(true);
        _collectImage.gameObject.SetActive(true);
        _damageImage.gameObject.SetActive(true);
        _healImage.gameObject.SetActive(true);

        LeanTween.scale (_faderImage, new Vector3 (1, 1, 1), 0);
        LeanTween.scale (_faderImage, Vector3.zero, 1f).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => {
            _faderImage.gameObject.SetActive(false);
        });
        
        LeanTween.alpha (_collectImage, 0, 0);
        LeanTween.alpha (_collectImage, 0, 0.5f).setOnComplete (() => {
            _collectImage.gameObject.SetActive (false);
        });

        LeanTween.alpha (_damageImage, 0, 0);
        LeanTween.alpha (_damageImage, 0, 0.5f).setOnComplete (() => {
            _damageImage.gameObject.SetActive (false);
        });

        LeanTween.alpha (_healImage, 0, 0);
        LeanTween.alpha (_healImage, 0, 0.5f).setOnComplete (() => {
            _healImage.gameObject.SetActive (false);
        });
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e) 
    {
        _faderImage.gameObject.SetActive (true);
        LeanTween.scale (_faderImage, Vector3.zero, 0f);
        LeanTween.scale (_faderImage, new Vector3 (1, 1, 1), 0.5f).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => {
            _deathSound.Play();
            _playerBody.SetActive(false);
            StartCoroutine(RestartLevelAfterDelay(0.3f));
            _damageParticleSystem.Play();
            _screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
        });
    }

    // private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    // {
    //     //_damageParticleSystem.Play();
    //     _damageImage.gameObject.SetActive(false);
    //     LeanTween.alpha(_damageImage, 1, 0);
    //     LeanTween.alpha(_damageImage, 0, 5f).setOnComplete(() => {
    //         _damageParticleSystem.Play();
    //     });
    // }

    // private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    // {
    //     //_healParticleSystem.Play();
    //     _damageImage.gameObject.SetActive(false);
    //     LeanTween.alpha(_damageImage, 1, 0);
    //     LeanTween.alpha(_damageImage, 0, 5f).setOnComplete(() => {
    //         _healParticleSystem.Play();
    //     });
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(SumScore.Score > _maxScore)
        {
            StartCoroutine(LoadNextlevelAfterDelay(0.5f));
        }

        switch(other.gameObject.tag)
        {
            case "Coin":
                SumScore.Add(_coinValue);
                _coinSound.Play();

                // Screen flashes red
                _collectImage.gameObject.SetActive(true);
                LeanTween.alpha(_collectImage, 1, 0);
                LeanTween.alpha(_collectImage, 0, 0.2f).setOnComplete(() => {
                    _coinParticleSystem.Play();
                });
                break;

            case "Enemy":
                _hurtSound.Play();
                healthSystem.Damage(_enemyDamage);
                _screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);

                // Screen flashes red
                _damageImage.gameObject.SetActive(true);
                LeanTween.alpha(_damageImage, 1, 0);
                LeanTween.alpha(_damageImage, 0, 0.2f).setOnComplete(() => {
                    _damageParticleSystem.Play();
                });
                break;
            
            case "Fungus":
                _hurtSound.Play();
                healthSystem.Damage(15f);
                _screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
                
                // Screen flashes red
                _damageImage.gameObject.SetActive(true);
                LeanTween.alpha(_damageImage, 1, 0);
                LeanTween.alpha(_damageImage, 0, 0.2f).setOnComplete(() => {
                    _damageParticleSystem.Play();
                });
                break;
            
            case "Heal":
                _healSound.Play();
                healthSystem.Heal(_healAmount);

                // Screen flashes green
                _healImage.gameObject.SetActive(true);
                LeanTween.alpha(_healImage, 1, 0);
                LeanTween.alpha(_healImage, 0, 0.2f).setOnComplete(() => {
                    _healParticleSystem.Play();
                });
                break;
            
            case "Death":
                _playerBody.SetActive(false);
                StartCoroutine(RestartLevelAfterDelay(0.5f));
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

