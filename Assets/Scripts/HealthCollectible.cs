using UnityEngine;

public class HealthCollectible : MonoBehaviour {

    [SerializeField] private AudioClip collectedClip;

    private void OnTriggerEnter2D(Collider2D other) {
        RubyController controller = other.GetComponent<RubyController>();
        if (controller) {
            if (controller.health < controller.maxHealth) {
                controller.ChangeHealth(1);
                Destroy(gameObject);

                controller.PlaySound(collectedClip);
            }
        }
    }
}
