using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private float outGround = 1000.0f;
    private Rigidbody2D rb2D;

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force) {
        rb2D.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D other) {

        EnemyController e = other.gameObject.GetComponent<EnemyController>();
        if (e)
            e.Fix();

        Destroy(gameObject);
    }

    private void Update() {
        if (transform.position.magnitude > outGround)
            Destroy(gameObject);
    }

}
