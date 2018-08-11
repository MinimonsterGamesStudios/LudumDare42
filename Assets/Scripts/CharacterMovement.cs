using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float _jumpForce = 50f;
    [SerializeField]
    private float _moveForce = 50f;
    private Rigidbody _rigidbody;
    private bool _isGrounded = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Jump();
        Move();
    }

    private void Jump()
    {
        bool shouldJump = Mathf.Abs(Input.GetAxis("Jump")) > 0.1f;
        if (_isGrounded && shouldJump)
        {
            _rigidbody.AddForce(new Vector3(0, 0, -1) * _jumpForce, ForceMode.Impulse);
        }
    }
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float dirX = 0, dirY = 0;

        if (Mathf.Abs(horizontalInput) > 0) {
            if ( horizontalInput > 0)
            {
                dirX = 1;
            } else
            {
                dirX = -1;
            }
        }

        if (Mathf.Abs(verticalInput) > 0)
        {
            if (verticalInput > 0)
            {
                dirY = 1;
            } else
            {
                dirY = -1;
            }
        }

        _rigidbody.AddForce(new Vector3(dirX, dirY) * _moveForce * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = true; 
        }

        if (collision.gameObject.tag == "Lava")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = false;
        }
    }
}
