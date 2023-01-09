using UnityEngine;
using System.Collections;
using FirstGearGames.SmoothCameraShaker;

namespace TarodevController
{
    public class PlayerDash : MonoBehaviour
    {
        [Header("Core Properties")]
        private IPlayerController _player;
        private float horizontal;
        private bool isFacingRight = true;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private TrailRenderer tr;

        [Header("Dash Properties")]
        private bool canDash = true;
        private bool isDashing;
        [SerializeField] private float dashingPower = 10f;
        [SerializeField] private float dashingTime = 0.1f;
        [SerializeField] private float dashingCooldown = 1f;

        [Header("Camera Properties")]
        [SerializeField] private ShakeData _screenShakeData = null;
        private ShakerInstance             _screenShakeInstance;
        
        void Awake() => _player = GetComponentInParent<IPlayerController>();

        private void Update()
        {
            if (isDashing) return;

            if (Input.GetKeyDown(KeyCode.Mouse0) && canDash)
            {
                StartCoroutine(Dash());
                _screenShakeInstance = CameraShakerHandler.Shake(_screenShakeData);
            }

            Flip();
        }

        private void FixedUpdate()
        {
            if (isDashing) return;
        }

        private void Flip()
        {
            if (_player.Input.X != 0) 
            {
                transform.localScale = new Vector3(_player.Input.X > 0 ? 1 : -1, 1, 1);
                isFacingRight = !isFacingRight;
            }
            
            // if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
            // {
            //     Vector3 localScale = transform.localScale;
            //     isFacingRight = !isFacingRight;
            //     localScale.x *= -1f;
            //     transform.localScale = localScale;
            // }
        }

        private IEnumerator Dash()
        {
            canDash = false;
            isDashing = true;

            float originalGravity = rb.gravityScale; // turning of gravity during dash
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f); // direction player is facing

            yield return new WaitForSeconds(dashingTime);
            rb.gravityScale = originalGravity;
            isDashing = false;

            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }
}