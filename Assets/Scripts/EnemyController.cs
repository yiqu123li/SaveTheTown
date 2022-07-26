using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] private ParticleSystem smokeEffect;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool vertical;//是否是纵向摆渡
    [SerializeField] private float changeTime = 3.0f;

    private Rigidbody2D rb2D;
    private Animator animator;

    private float timer;
    private int direction = 1;
    private bool broken = true;


    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
    }

    private void Update() {
        if (!broken)
            return;

        timer -= Time.deltaTime;
        if (timer < 0) {
            direction = -direction;
            timer = changeTime;
        }
    }

    private void FixedUpdate() {

        if (!broken)
            return;

        Vector2 pos = rb2D.position;
        if (vertical) {
            pos.y += Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        } else {
            pos.x += Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        rb2D.MovePosition(pos);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if (player)
            player.ChangeHealth(-1);
    }


    public void Fix() {
        broken = false;
        rb2D.simulated = false;
        animator.SetTrigger("Fixed");
        //smokeEffect.Stop();//关闭循环播放,改成了粒子播放玩自动销毁
    }
}
