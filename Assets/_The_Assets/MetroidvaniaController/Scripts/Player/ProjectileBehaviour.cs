using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour
{
    [Header("Core Properties")]
	[SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _bulletRB;

	private void Start()
	{
        _bulletRB.velocity = transform.right * _speed;
        Destroy(gameObject, 3f);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     switch(other.gameObject.tag)
    //     {
    //         case "Enemy":
    //             Destroy(gameObject)
    //             break;
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Enemy":
                Destroy(gameObject);
                break;
        }
    }
}
