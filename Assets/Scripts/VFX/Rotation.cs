using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _angle = 1;
    [SerializeField] private Vector3 _axis;
    void Update()
    {
        transform.Rotate(_axis, _angle * Time.deltaTime);
    }
}
