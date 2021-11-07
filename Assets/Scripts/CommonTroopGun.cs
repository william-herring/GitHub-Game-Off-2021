using System;
using Pathfinding;
using UnityEngine;

public class CommonTroopGun : MonoBehaviour
{
    private void Update()
    {
        Vector2 target = transform.root.gameObject.GetComponent<AIDestinationSetter>().target.position;
        
        float angle = Mathf.Atan2(target.y, target.x) * 0.8f * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
