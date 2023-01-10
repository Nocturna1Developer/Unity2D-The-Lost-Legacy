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
    [SerializeField] private int _fungusDamage = 15;
    [SerializeField] private int _enemyDamage = 30;
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
    [SerializeField] private ParticleSystem _coinParticleSystem;

    [Header("Audio Properties")]
    [SerializeField] private AudioSource _coinSound;
    [SerializeField] private AudioSource _deathSound; 
    [SerializeField] private AudioSource _hurtSound;   

    private void Awake()
    {
        healthSystem = new HealthSystem(100);
        healthSystem.OnDead += HealthSystem_OnDead;

        _faderImage.gameObject.SetActive(true);
        LeanTween.alpha(_faderImage, 1, 0);
        LeanTween.alpha(_faderImage, 0, 2f).setOnComplete(() => {
            _faderImage.gameObject.SetActive(false);
        });
    }

    private void FixedUpdate()
    {
        if(UnityEngine.Input.GetButtonDown("Jump")) 
        {
            //Debug.Log("this is being called");
            //_screenShakeInstance = CameraShakerHandler.Shake(_jumpShakeData);
        }
        else
        {
            _screenShakeInstance= null;
        }
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
        StartCoroutine(RestartLevelAfterDelay(1f));
        _damageParticleSystem.Play();

        _screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        _damageParticleSystem.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(SumScore.Score > _maxScore)
        {
            StartCoroutine(LoadNextlevelAfterDelay(1f));
        }

        switch(other.gameObject.tag)
        {
            case "Coin":
                //Debug.Log("TOUCHED"); 
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
                healthSystem.Damage(_fungusDamage);
                _damageParticleSystem.Play();
                _screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
                break;
            
            case "Death":
                _playerBody.SetActive(false);
                StartCoroutine(RestartLevelAfterDelay(1f));
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

