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

	[Header("Particle Properties")]
	[SerializeField] private ParticleSystem _dashParticleSystem;

	[Header("Mobile Properties")]
	[SerializeField] private Joystick _moveJumpjoystick;

	//bool dashAxis = false;
	
	// Update is called once per frame
	// void Update () {

	// 	horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

	// 	animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

	// 	if (Input.GetKeyDown(KeyCode.W))
	// 	{
	// 		jump = true;
	// 	}

	// 	if (Input.GetKeyDown(KeyCode.Z))
	// 	{
	// 		_screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
    //         //_dashSound.Play();
    //         _dashParticleSystem.Play();
	// 		dash = true;
	// 	}

	// 	/*if (Input.GetAxisRaw("Dash") == 1 || Input.GetAxisRaw("Dash") == -1) //RT in Unity 2017 = -1, RT in Unity 2019 = 1
	// 	{
	// 		if (dashAxis == false)
	// 		{
	// 			dashAxis = true;
	// 			dash = true;
	// 		}
	// 	}
	// 	else
	// 	{
	// 		dashAxis = false;
	// 	}
	// 	*/

	// }

	private void Update ()
	{
		horizontalMove = _moveJumpjoystick.Horizontal * runSpeed;
		float verticalMove = _moveJumpjoystick.Vertical;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (verticalMove > .5f)
		{
			jump = true;
		}

		// float horizontalMoveDash = _dashAttackJoystick.Horizontal; 
		// if (Input.GetKeyDown(KeyCode.Z))
		// {
		// 	_screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
        //     _dashParticleSystem.Play();
		// 	dash = true;
		// }
	}

	public void dashClick()
	{
		_screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
        _dashParticleSystem.Play();
		dash = true;
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
