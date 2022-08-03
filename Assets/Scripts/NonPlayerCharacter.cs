using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour {
    [SerializeField] private float displayTime = 4f;
    [SerializeField] private GameObject dialogBox;
    private float timerDisplay = -1.0f;

    // Start is called before the first frame update
    private void Start() {
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    private void Update() {
        if (timerDisplay >= 0) {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
                dialogBox.SetActive(false);
        }
    }

    public void DisplayDialog() {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}
