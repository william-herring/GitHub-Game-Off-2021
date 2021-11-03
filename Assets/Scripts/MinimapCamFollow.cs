using System;
using UnityEngine;

public class MinimapCamFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -5);
    }
}
