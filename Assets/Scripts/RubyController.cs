using UnityEngine;

public class RubyController : MonoBehaviour {
    [SerializeField] private float speed = 3.0f;//主角移动速度
    public int maxHealth = 5;//最大生命值容量
    [SerializeField] private float timeInvincible = 2.0f;//无敌时间上限
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AudioClip launchClip;
    [SerializeField] private AudioClip injuredClip;
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject talkButton;


    public int health { get => currentHealth; }
    private int currentHealth;//当前主角生命值
    private bool isInvincible;//当前是否处于无敌状态
    private float invincibleTimer;//当前剩下的无敌时间

    private Rigidbody2D rb2D;
    private float horizontal;
    private float vertical;

    private Animator animator;
    private Vector2 lookDirection = new Vector2(1, 0);//她站立不动时，Move X 和 Y 均为 0，因此状态机不知道要使用哪个方向（除非我们指定方向）。

    private AudioSource audioSource;

    private RaycastHit2D hit;//当前射线投射到的目标

    //private void Awake() {
    //    rb2D = GetComponent<Rigidbody2D>();
    //    animator = GetComponent<Animator>();
    //    audioSource = GetComponent<AudioSource>();
    //}

    // Start is called before the first frame update
    private void Start() {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update() {
        //horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");

        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0)) {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible) {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Launch();

        //if (Input.GetKeyDown(KeyCode.X)) {
        //    
        //    RaycastHit2D hit = Physics2D.Raycast(rb2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
        //    if (hit.collider) {
        //        Debug.Log("青蛙应该跟你对话");
        //        NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
        //        if (character)
        //            character.DisplayDialog();
        //    }
        //}

        /*射线投射是将射线投射到场景中并检查该射线是否与碰撞体相交的行为*/
        hit = Physics2D.Raycast(rb2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider) {
            if (hit.collider.gameObject.name == "Jambi")
                talkButton.SetActive(true);
        } else {
            talkButton.SetActive(false);
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
            animator.SetTrigger("Hit");

            if (isInvincible)
                return;

            audioSource.PlayOneShot(injuredClip);

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    public void Launch() {
        GameObject gameObject = Instantiate(projectilePrefab, rb2D.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = gameObject.AddComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        PlaySound(launchClip);
    }

    public void TalkSomeOne() {
        if (hit.collider) {
            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (character)
                character.DisplayDialog();
        }
    }

    public void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }
}
