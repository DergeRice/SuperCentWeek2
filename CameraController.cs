using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//로티
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _target;
    Vector3 _offset;

    [Space]
    public Animator _anim;


    [SerializeField] float _moveSpeed;

    [SerializeField] float _positionXLimit;
    [SerializeField] float _positionZLimit;

    void Start()
    {
        _offset = transform.position;
    }

    void Update()
    {
        /*
        transform.position = Vector3.Lerp(transform.position,
        new Vector3(LimitPositionX(_target.position.x, _positionXLimit), _target.position.y, LimitPositionZ(_target.position.z, _positionZLimit))
        , _moveSpeed * Time.deltaTime);*/

        transform.position = Vector3.Lerp(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);


    }

    private float LimitPositionX(float position, float positionXLimit)
    {
        var newPosition = position;
        newPosition = Mathf.Clamp(newPosition, -positionXLimit, positionXLimit);
        return newPosition;
    }

    private float LimitPositionZ(float position, float positionZLimit)
    {
        var newPosition = position;
        newPosition = Mathf.Clamp(newPosition, -positionZLimit, positionZLimit);
        return newPosition;
    }

    public void StartCameraAnim()
    {
        _anim.SetTrigger("Start");
    }


}
