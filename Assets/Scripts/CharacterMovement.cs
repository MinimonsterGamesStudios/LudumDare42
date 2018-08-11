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

    private void Update()
    {
        Jump();
        Move();
    }

    private void Jump()
    {
        bool shouldJump = Mathf.Abs(Input.GetAxis("Jump")) > 0.1f;
        if (_isGrounded && shouldJump)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce);
        }
    }
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
        {
            _rigidbody.AddForce(new Vector3(horizontalInput, 0, verticalInput) * _moveForce, ForceMode.VelocityChange);
        }
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
