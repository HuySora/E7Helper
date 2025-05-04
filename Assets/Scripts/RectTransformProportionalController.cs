using UnityEngine;

public class RectTransformProportionalController : MonoBehaviour {
    [field: Header("References")]
    [field: SerializeField] public RectTransform ParentRect { get; private set; }

    [field: Header("Runtime")]
    [field: SerializeField] public RectTransform RectTransform { get; private set; }

    [field: SerializeField] public Vector2 InitialSize { get; private set; }
    [field: SerializeField] public Vector3 InitialScale { get; private set; }
    [field: SerializeField] public float ScaleX { get; private set; }
    [field: SerializeField] public float ScaleY { get; private set; }

    private void Awake() {
        RectTransform = GetComponent<RectTransform>();
        if (ParentRect == null || RectTransform == null) {
            enabled = false;
            return;
        }

        var parentSizeX = ParentRect.rect.size.x;
        var parentSizeY = ParentRect.rect.size.y;

        ScaleX = parentSizeX != 0 ? RectTransform.anchoredPosition.x / parentSizeX : 0;
        ScaleY = parentSizeY != 0 ? RectTransform.anchoredPosition.y / parentSizeY : 0;

        InitialSize = new Vector2(
            RectTransform.sizeDelta.x / parentSizeX,
            RectTransform.sizeDelta.y / parentSizeY
        );

        InitialScale = RectTransform.localScale;
    }

    private void Update() {
        var parentSizeX = ParentRect.rect.size.x;
        var parentSizeY = ParentRect.rect.size.y;

        // Update position proportionally
        var posX = ScaleX * parentSizeX;
        var posY = ScaleY * parentSizeY;
        RectTransform.anchoredPosition = new Vector2(posX, posY);

        // Calculate scale factors based on parent size and initial size
        float scaleX = (InitialSize.x * parentSizeX) / RectTransform.rect.width;
        float scaleY = (InitialSize.y * parentSizeY) / RectTransform.rect.height;

        // Apply scale relative to initial scale to preserve original scale proportions
        Vector3 newScale = new Vector3(
            InitialScale.x * scaleX,
            InitialScale.y * scaleY,
            InitialScale.z
        );
        RectTransform.localScale = newScale;
    }
}