using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform shootPoint;   

    private FlyEnemyAI _ai;

    void Start()
    {
        _ai = GetComponent<FlyEnemyAI>();
        
        if (_ai != null)
        {
            _ai.Fire += Shoot; 
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && shootPoint != null)
        {
            Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        }
    }
}