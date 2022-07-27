using UnityEngine;

public class RubyController : MonoBehaviour {
    [SerializeField] private float speed = 3.0f;

    public int maxHealth = 5;
    public int health { get => currentHealth; }

    private int currentHealth;

    private Rigidbody2D rb2D;
    private float horizontal;
    private float vertical;

    // Start is called before the first frame update
    private void Start() {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;

        rb2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        Vector2 position = rb2D.position;
        position.x = speed * horizontal * Time.deltaTime + position.x;
        position.y = speed * vertical * Time.deltaTime + position.y;
        rb2D.MovePosition(position);
    }

    public void ChangeHealth(int amount) {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
