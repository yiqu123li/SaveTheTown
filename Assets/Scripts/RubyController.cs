using UnityEngine;

public class RubyController : MonoBehaviour {

    private Rigidbody2D rb2D;
    private float horizontal;
    private float vertical;

    // Start is called before the first frame update
    private void Start() {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;

        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        Vector2 position = rb2D.position;
        position.x = 3.0f * horizontal * Time.deltaTime + position.x;
        position.y = 3.0f * vertical * Time.deltaTime + position.y;
        rb2D.MovePosition(position);
    }
}
