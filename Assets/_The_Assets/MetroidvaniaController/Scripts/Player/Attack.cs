using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FirstGearGames.SmoothCameraShaker;

public class Attack : MonoBehaviour
{
	[Header("Core Properties")]
	[SerializeField] private GameObject _swordSlash;
	//[SerializeField] private ProjectileBehaviour _swordSlash;
	[SerializeField] private Transform attackCheck;
	[SerializeField] private bool canAttack = true;
	[SerializeField] private bool isTimeToCheck = false;
	[SerializeField] private float _attackCoolDown = 1f;

	private Rigidbody2D m_Rigidbody2D;

	[Header("Animator Properties")]
	[SerializeField] private Animator animator;

	[Header("Camera Properties")]
	[SerializeField] private GameObject cam;
	[SerializeField] private ShakeData _screenShakeData = null;
	private ShakerInstance             _screenShakeInstance;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Mouse1) && canAttack)
		{
			canAttack = false;
			animator.SetBool("IsAttacking", true);
			StartCoroutine(AttackCooldown());
			_screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
			Instantiate(_swordSlash, attackCheck.position, attackCheck.rotation);
		}

		// if (Input.GetKeyDown(KeyCode.Mouse1))
		// {
		// 	//GameObject throwableWeapon = Instantiate(throwableObject, transform.position + new Vector3(transform.localScale.x * 0.5f,-0.2f), Quaternion.identity) as GameObject; 
		// 	GameObject throwableWeapon = Instantiate(throwableObject, attackCheck.position, attackCheck.rotation);
		// 	Vector2 direction = new Vector2(transform.localScale.x, 0);
		// 	throwableWeapon.GetComponent<ThrowableWeapon>().direction = direction; 
		// 	throwableWeapon.name = "ThrowableWeapon";
		// }
	}

	IEnumerator AttackCooldown()
	{
		yield return new WaitForSeconds(_attackCoolDown);
		canAttack = true;
	}
}
