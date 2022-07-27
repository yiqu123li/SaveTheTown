using UnityEngine;

public class RubyController : MonoBehaviour {
    [SerializeField] private float speed = 3.0f;//主角移动速度

    public int maxHealth = 5;//最大生命值容量
    public float timeInvincible = 2.0f;//无敌时间上限
    public int health { get => currentHealth; }
    private int currentHealth;//当前主角生命值
    private bool isInvincible;//当前是否处于无敌状态
    private float invincibleTimer;//当前剩下的无敌时间

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

        if (isInvincible) {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    private void FixedUpdate() {
        Vector2 position = rb2D.position;
        position.x = speed * horizontal * Time.deltaTime + position.x;
        position.y = speed * vertical * Time.deltaTime + position.y;
        rb2D.MovePosition(position);
    }

    public void ChangeHealth(int amount) {
        if (amount < 0) {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
