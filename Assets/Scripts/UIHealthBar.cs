using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour {
    public static UIHealthBar instance { get; private set; }

    public Image mask;
    private float originalSize;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start() {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value) {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

}
