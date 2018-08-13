using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RigibodyCharacter : MonoBehaviour
{
    public AudioClip rockLand;

    public float movementSpeed = 5f;
    public float jumpMovementSpeed = 2f;
    private float currentMovementSpeed = 0;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public LayerMask Ground;

    private Rigidbody _body;
    private AudioSource _audioSource;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    private bool _jumpPressed = false;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.clip = rockLand;
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);


        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.y = Input.GetAxis("Vertical");
        if (_inputs != Vector3.zero)
        {
            transform.up = _inputs;
        }

        // Check if player is falling into the abyss
        if (!_isGrounded && transform.position.z >= 1)
        {
            GameOver();
        }
    }


    void FixedUpdate()
    {
        if (Mathf.Abs(Input.GetAxis("Jump")) > 0 && _isGrounded && !_jumpPressed)
        {
            var jumpForce = Vector3.back * Mathf.Sqrt(JumpHeight * 2f * Physics.gravity.z);
            _body.AddForce(jumpForce, ForceMode.VelocityChange);
            _jumpPressed = true;
            _body.MovePosition(_body.position + _inputs * jumpMovementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            if(Input.GetAxisRaw("Aim") != 1)
            {
                _body.MovePosition(_body.position + _inputs * movementSpeed * Time.fixedDeltaTime);
                _jumpPressed = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lava" || collision.gameObject.tag == "Enemy")
        {
            GameOver();
        }
        else if(collision.gameObject.tag == "Ground")
        {
            _audioSource.clip = rockLand;
            _audioSource.Play();
        }
    }

    void GameOver()
    {
        GroundBreaker.ResetTimeValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}