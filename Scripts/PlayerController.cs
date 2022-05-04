using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]



public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _movespeed;

    [SerializeField] CinemachineFreeLook thirdPersonCam;
    [SerializeField] CinemachineFreeLook firstPersonCam;


    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * _movespeed, _rigidbody.velocity.y, _joystick.Vertical * _movespeed);

        if(_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _animator.SetBool("WalkNormal", true);
        }
        else
        {
            _animator.SetBool("WalkNormal", false);
        }
    }

    private void OnEnable()
    {
        CameraSwitch.Register(thirdPersonCam);
        CameraSwitch.Register(firstPersonCam);
        CameraSwitch.SwitchCamera(thirdPersonCam);
    }

    private void OnDisable()
    {
        CameraSwitch.Unregister(thirdPersonCam);
        CameraSwitch.Unregister(firstPersonCam);
    }
    public void SwitchPerson()
    {
        if (CameraSwitch.isActiveCamera(thirdPersonCam))
        {
            CameraSwitch.SwitchCamera(firstPersonCam);
        }
        else if (CameraSwitch.isActiveCamera(firstPersonCam))
        {
            CameraSwitch.SwitchCamera(thirdPersonCam);
        }
    }
}
