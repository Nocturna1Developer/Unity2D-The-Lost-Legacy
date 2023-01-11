using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FirstGearGames.SmoothCameraShaker;

public class PlayerMovement : MonoBehaviour
{
	[Header("Core Properties")]
	public CharacterController2D controller;
	public Animator animator;
	public float runSpeed = 40f;
	float horizontalMove = 0f;
	bool jump = false;
	bool dash = false;

	[Header("Camera Properties")]
	[SerializeField] private ShakeData _screenShakeData = null;
	private ShakerInstance             _screenShakeInstance;

	//[Header("Audio Properties")]
	//[SerializeField] private AudioSource _dashSound;

	[Header("Particle Properties")]
	[SerializeField] private ParticleSystem _dashParticleSystem;

	//bool dashAxis = false;
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetKeyDown(KeyCode.Space))
		{
			jump = true;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			_screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
            //_dashSound.Play();
            _dashParticleSystem.Play();
			dash = true;
		}

		/*if (Input.GetAxisRaw("Dash") == 1 || Input.GetAxisRaw("Dash") == -1) //RT in Unity 2017 = -1, RT in Unity 2019 = 1
		{
			if (dashAxis == false)
			{
				dashAxis = true;
				dash = true;
			}
		}
		else
		{
			dashAxis = false;
		}
		*/

	}

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
		jump = false;
		dash = false;
	}
}
