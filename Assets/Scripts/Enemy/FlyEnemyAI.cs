using UnityEngine;

public class FlyEnemyAI : MonoBehaviour
{
    enum EFly { Move, Attack }
    FSM<EFly> brain;

    [Header("Referencies")]
    public Transform Player;
    public Transform UpPoint;
    public Transform LateralPoint;
    public Transform DownPoint;

    [Header("Configuració")]
    public float speed = 3.0f;
    public float attackDistance = 10.0f;
    public float radiusDetectWalls = 0.25f;
    [SerializeField] LayerMask Ground;
    [SerializeField] Vector2 direction = new Vector2(-1, 0.25f);

    private Rigidbody2D _rigidbody;
    bool upHit, lateralHit, downHit;
    float timerToShoot;

    public System.Action Fire;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        InitFSM();
    }

    void Update()
    {
        brain.Update();
    }

    void InitFSM()
    {
        brain = new FSM<EFly>(EFly.Move);
        brain.SetOnEnter(EFly.Attack, () => timerToShoot = 0f);
        brain.SetOnStay(EFly.Move, MoveUpdate);
        brain.SetOnStay(EFly.Attack, AttackUpdate);
    }

    void MoveUpdate()
    {
        _rigidbody.linearVelocity = direction * speed;

        if (DetectCollision())
            ChangeDirection();

        if (IsPlayerCloseByDistance(attackDistance))
            brain.ChangeState(EFly.Attack);
    }

    void AttackUpdate()
    {
        Vector2 dir = ((Vector2)Player.position - (Vector2)transform.position).normalized;
        _rigidbody.linearVelocity = dir * speed;

        timerToShoot += Time.deltaTime;
        if (timerToShoot >= 1.5f)
        {
            Fire?.Invoke();
            timerToShoot = 0f;
        }

        if (!IsPlayerCloseByDistance(attackDistance))
            brain.ChangeState(EFly.Move);
    }

    bool DetectCollision()
    {
        upHit      = Physics2D.OverlapCircle(UpPoint.position,      radiusDetectWalls, Ground);
        lateralHit = Physics2D.OverlapCircle(LateralPoint.position,  radiusDetectWalls, Ground);
        downHit    = Physics2D.OverlapCircle(DownPoint.position,     radiusDetectWalls, Ground);
        return upHit || lateralHit || downHit;
    }

    void ChangeDirection()
    {
        if (lateralHit)
        {
            transform.Rotate(0, 180, 0);
            direction.x = -direction.x;
        }
        if (upHit && direction.y > 0)
            direction.y = -direction.y;
        if (downHit && direction.y < 0)
            direction.y = -direction.y;
    }

    bool IsPlayerCloseByDistance(float distance)
    {
        return Vector2.Distance(transform.position, Player.position) < distance;
    }
}
