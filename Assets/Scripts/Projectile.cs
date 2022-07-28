using UnityEngine;

public class Projectile : MonoBehaviour {
    private Rigidbody2D rb2D;

    private SpriteRenderer rbSprite;

    // Start is called before the first frame update
    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force) {
        rb2D.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("与子弹碰撞的是" + other.gameObject);
        Destroy(gameObject);
    }

}
