using UnityEngine;
using UnityEngine.SceneManagement; 

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 _moveDirection;

    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        if (player != null)
        {
            _moveDirection = (player.position - transform.position).normalized;
        }

        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(_moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
            Time.timeScale = 1f; 
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}