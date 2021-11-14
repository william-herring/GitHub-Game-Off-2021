using System;
using UnityEngine;

public class MinimapCamFollow : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -5);
    }
}
