using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class HeroMovement : MonoBehaviour
{

    [SerializeField] private float _agentAccuracy = 1;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    private event Action<HeroMovement> OnStopEvent = null;
    private event Action OnGrabEvent = null;
    public bool ControllerEnabled { get; set; } = true;


    public void GoToPoint(Vector3 point, Action<object> OnStop = null)
    {
        if (!ControllerEnabled) return;
        StopAllCoroutines();

        OnStopEvent = null;
        OnStopEvent += OnStop;
        var path = new NavMeshPath();
        _agent.CalculatePath(point, path);
        _agent.SetPath(path);
        _animator.SetBool("isWalking", true);    
        StartCoroutine("CheckPath");
    }

    public void GoToPoint(Vector3 point, Quaternion rotation, Action<object> OnStop = null)
    {
        if (!ControllerEnabled) return;
        StopAllCoroutines();
        OnStopEvent = null;
        OnStopEvent += OnStop;
        var path = new NavMeshPath();
        _agent.CalculatePath(point, path);
        _agent.SetPath(path);
        _animator.SetBool("isWalking", true);
        StartCoroutine("CheckPathWithRotation",rotation);
    }



    public void ResetMovement()
    {
        StopCoroutine("CheckPath");
        _animator.SetBool("isWalking", false);
    }


    public void PlayTakeAnimation(Vector3 itemPosition,Action OnItemGrabbed)
    {
        OnGrabEvent = null;
        OnGrabEvent += OnItemGrabbed;
        GoToPoint(itemPosition, ReadyToTakeItem);
    }

    private void ReadyToTakeItem(object param)
    {
        ControllerEnabled = false;
        _animator.SetBool("isGrabbing", true);
    }

    public void GrabItem()
    {
        _animator.SetBool("isGrabbing", false);
        OnGrabEvent();
        OnGrabEvent = null;
        ControllerEnabled = true;
    }

    public void SetupHeroPosition(Vector3 value)
    {
        StopCoroutine("CheckPath");
        _agent.enabled = false;
        transform.position = value;
        _agent.enabled = true;
    }

    public void SetupHeroRotation(Quaternion rotation)
    {
        StopCoroutine("CheckPath");
        _agent.enabled = false;
        transform.rotation = rotation;
        _agent.enabled = true;
    }

    private IEnumerator CheckPath()
    {
        while (_agent.remainingDistance > _agentAccuracy)
        {
            yield return null;
        }      
        _animator.SetBool("isWalking", false);
        OnStopEvent?.Invoke(this);
    }

    private IEnumerator CheckPathWithRotation(Quaternion rotation)
    {
        float distance = _agent.remainingDistance - _agentAccuracy;
        float half = distance / 2;
        Quaternion startRotation = transform.rotation;

        while (_agent.remainingDistance > _agentAccuracy)
        {
            if (distance - _agent.remainingDistance >= half)
            {
                float percent = (half - _agent.remainingDistance) / half;

                transform.rotation = Quaternion.Lerp(startRotation, rotation, percent);
            }
            else
            {
                startRotation = transform.rotation;
            }
            
            yield return null;
        }
        _animator.SetBool("isWalking", false);

        StartCoroutine("Wait",rotation);
        
    }

    private IEnumerator Wait(Quaternion rotation)
    {
        float time = 0;
        while (time <= 0.3f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 10 * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        OnStopEvent?.Invoke(this);

    }
}
