using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DASH : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Collider2D _collider;
    private TrailRenderer _trailRenderer;
 
    [Header("Movement variables")]
    [SerializeField] private bool _active = true;
    [SerializeField] private float _movementVel = 3f;
    [SerializeField] private float _jumpVel = 10f;

    [Header("Dashing")]
    [SerializeField] private float _dashingVelocity = 14f;
    [SerializeField] private float _dashingTime = 0.5f;
    private Vector2 _dashingDir;
    private bool _isDashing;
    private bool _canDash;
    private float inputX;
    private bool jumpInput;
    private bool jumpInputReleased;
    private bool dashInput;

    private void Start()


    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
        // SetRespawnPoint(transform);

    }

    private void Update()
    {
         if (!_active)
            return;
 
          inputX = Input.GetAxisRaw("Horizontal");
          jumpInput = Input.GetButtonDown("Jump");
         jumpInputReleased = Input.GetButtonUp("Jump");
        dashInput = Input.GetButtonDown("Dash");

         if(dashInput && _canDash)

        {
            _isDashing = true;
            _canDash = false;
            _trailRenderer.emitting = true;
            _dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            // if (_dashingDir = new Vector2(transform.localScale.x, 0);

        }

        //Add stopping dash


       /*if (_isDashing)
        {
          -rigidbody.velocity = _dashingDir.normalized * _dashingVelocity;
          return;
          
       }*/
      
     }
}







