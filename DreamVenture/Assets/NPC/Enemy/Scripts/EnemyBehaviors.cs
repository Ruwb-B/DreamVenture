using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviors : MonoBehaviour
{
    [SerializeField] PlayerDetector _playerDetector;
    StickmanBehaviors _behaviors;

    [Header("Movement")]
    [SerializeField] float _movementForce = 700f;
    [SerializeField] float _jumpForce = 27000f;
    [SerializeField] float _waitBetweenJump = 1f;
    [SerializeField] float _keepDistance = 5f;
    // For lowering directionToTarget.y to prevent it from jumping 
    [SerializeField] float _offsetHeight = 1f;
    //[SerializeField] float _crouchDecreaseMovement = 0.5f;

    [Space(5)]
    [Range(0f, 100f)]
    [SerializeField] float _raycastDistance = 1.4f;

    [Header("Animation")]
    [SerializeField] Animator _animator;

    float _moveDirection = 0;
    bool _jump = false;
    bool _ableJump = true;
    //bool _crouch;
    //bool _isCrouching = false;

    [Header("Combat")]
    EnemyArmController[] _armControllers;
    [SerializeField] float[] _armControllDuration = { 1f, 2f };
    [SerializeField] float[] _armControllDelay = { 2f, 3f };
    bool _isArmControllingOn = false;

    private void Awake()
    {
        _behaviors = this.GetComponent<StickmanBehaviors>();
        _behaviors.setRaycastDistance(_raycastDistance);
        _armControllers = GetComponentsInChildren<EnemyArmController>();
    }

    private void FixedUpdate()
    {
        if (_playerDetector._playerDetected)
        {
            PrepareActions();
            Action();
            if (!_isArmControllingOn)
            {
                StartCoroutine("ArmController");
            }
        }
        else if (_isArmControllingOn)
        {
            StopCoroutine("ArmController");
        }
    }

    void PrepareActions()
    {
        //Move side way
        Vector2 direction = _playerDetector._directionToTarget;
        if (direction.x < 0 && direction.x < -_keepDistance)
        {
            _moveDirection = -1;
        }
        else if (direction.x > 0 && direction.x > _keepDistance)
        {
            _moveDirection = 1;
        }
        else
        {
            _moveDirection = 0;
        }

        //Jump
        if (direction.y > transform.position.y + _offsetHeight)
        {
            _jump = true;
        }

        //if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    _crouch = true;
        //}
        //else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        //{
        //    _crouch = false;
        //}
    }

    void Action()
    {
        _behaviors.Move(_movementForce, _moveDirection);

        //Animation
        if (_moveDirection != 0)
        {
            _animator.SetBool("Walk", true);
        }
        else
        {
            _animator.SetBool("Walk", false);
        }

        if (_jump && _ableJump)
        {
            _behaviors.Jump(_jumpForce);
            StartCoroutine(JumpDelay());
            _jump = false;
        }

        //if (_crouch && !_isCrouching)
        //{
        //    _behaviors.StartCrouching();
        //    _movementForce *= _crouchDecreaseMovement;
        //    _isCrouching = true;
        //}
        //else if (!_crouch && _isCrouching)
        //{
        //    _behaviors.StopCrouching();
        //    _movementForce /= _crouchDecreaseMovement;
        //    _isCrouching = false;
        //}
    }

    IEnumerator ArmController()
    {
        TurnOnArmController();
        yield return new WaitForSeconds(Random.Range(_armControllDuration[0], _armControllDuration[1]));
        TurnOffArmController();
        yield return new WaitForSeconds(Random.Range(_armControllDelay[0], _armControllDelay[1]));
        StartCoroutine("ArmController");
    }

    void TurnOnArmController()
    {
        foreach (EnemyArmController armController in _armControllers)
        {
            armController.enabled = true;
        }
    }
    void TurnOffArmController()
    {
        foreach (EnemyArmController armController in _armControllers)
        {
            armController.enabled = false;
        }
    }

    public void StopArmControll()
    {
        StopCoroutine("ArmController");
        TurnOffArmController();
    }

    IEnumerator JumpDelay()
    {
        _ableJump = false;
        yield return new WaitForSeconds(_waitBetweenJump);
        _ableJump = true;
    }
}
