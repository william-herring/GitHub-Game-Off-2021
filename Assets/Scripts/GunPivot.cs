using UnityEngine;

public class GunPivot : MonoBehaviour
{
    private void Update()
    {
        PointToMouse();
    }
    
    private void PointToMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 5.23f;

        Vector3 objPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x = mousePosition.x - objPosition.x;
        mousePosition.y = mousePosition.y - objPosition.y;
 
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * 0.8f * Mathf.Rad2Deg; //Don't ask me how this actually works
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
