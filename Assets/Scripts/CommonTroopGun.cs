using System;
using Pathfinding;
using UnityEngine;

public class CommonTroopGun : MonoBehaviour
{
    private void Update()
    {
        if (transform.root.gameObject.GetComponent<AIDestinationSetter>() == null) return;
        Vector2 target = transform.root.gameObject.GetComponent<AIDestinationSetter>().target.position;

        float angle = Mathf.Atan2(target.y, target.x) * 0.8f * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(transform.root.gameObject.CompareTag("Team")
            ? new Vector3(0, 0, angle - 90)
            : new Vector3(0, 0, angle - 22.5f));
    }
}
