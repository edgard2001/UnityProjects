using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _jumpHeight = 2f, _jumpTimeSeconds = 0.5f, _moveSpeed = 4f;

    private Rigidbody2D _playerBody;
    private GroundCheck _groundCheck;
    private Animator _animation;
    private SpriteRenderer _renderer;

    private Vector2 _verticalVelocity;
    private float _jumpStartVelocity;


    private void Awake()
    {
        _playerBody = transform.Find("Player Character").GetComponentInChildren<Rigidbody2D>();
        _groundCheck = transform.Find("Player Character").Find("Ground Check").GetComponentInChildren<GroundCheck>();
        _animation = transform.Find("Player Character").GetComponentInChildren<Animator>();
        _renderer = transform.Find("Player Character").GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        _jumpStartVelocity = 2 * _jumpHeight / _jumpTimeSeconds;
        _playerBody.gravityScale = (_jumpStartVelocity / _jumpTimeSeconds) / (-1 * Physics2D.gravity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (_groundCheck.IsGrounded && Input.GetKey(KeyCode.W))
        {
            _verticalVelocity = _jumpStartVelocity * Vector2.up;
            _animation.Play("Idle");
        }
        else
        {
            _verticalVelocity = _playerBody.velocity.y * Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _playerBody.velocity = _moveSpeed * Vector2.left + _verticalVelocity;
            _renderer.flipX = true;
            _animation.Play("Run");
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _playerBody.velocity = _moveSpeed * Vector2.right + _verticalVelocity;
            _renderer.flipX = false;
            _animation.Play("Run");
        }
        else
        {
            _playerBody.velocity = 0 * Vector2.right + _verticalVelocity;
            _animation.Play("Idle");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _animation.Play("Attack");
        }
       
    }

}
