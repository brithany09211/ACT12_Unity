using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void LateUpdate()
    {
        if (target != null)
        {
            float clampX = Mathf.Clamp(target.position.x, minX, maxX);
            float clampY = Mathf.Clamp(target.position.y, minY, maxY);
            
            transform.position = new Vector3(clampX, clampY, transform.position.z);
        }
    }
}