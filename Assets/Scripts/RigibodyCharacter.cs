using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RigibodyCharacter : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public LayerMask Ground;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    private bool _jumpPressed = false;
    void Start()
    {
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
            transform.up = _inputs;

        if (Mathf.Abs(Input.GetAxis("Jump")) > 0 && _isGrounded && !_jumpPressed)
        {
            var jumpForce = Vector3.back * Mathf.Sqrt(JumpHeight * 2f * Physics.gravity.z);
            _body.AddForce(jumpForce, ForceMode.VelocityChange);
            _jumpPressed = true;
        }
        else
        {
            _jumpPressed = false;
        }
    }


    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}