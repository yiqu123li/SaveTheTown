using UnityEngine;

public class DamageZone : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D other) {
        RubyController controller = other.GetComponent<RubyController>();
        if (controller) {
            controller.ChangeHealth(-1);
        }
    }
}
