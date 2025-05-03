using UnityEngine;

public class SetRandomPosition : MonoBehaviour {
    private RectTransform m_CanvasRectTransform;
    private RectTransform m_RectTransform;

    private void Start() {
        m_CanvasRectTransform = m_CanvasRectTransform == null ? GetComponentInParent<Canvas>().GetComponent<RectTransform>() : null;
        m_RectTransform = m_RectTransform == null ? GetComponent<RectTransform>() : null;
    }

    public void Trigger() {
        var canvasSize = m_CanvasRectTransform.rect.size;
        var buttonSize = m_RectTransform.rect.size;

        var minX = -canvasSize.x / 2 + buttonSize.x / 2;
        var maxX = canvasSize.x / 2 - buttonSize.x / 2;

        var minY = -canvasSize.y / 2 + buttonSize.y / 2;
        var maxY = canvasSize.y / 2 - buttonSize.y / 2;

        var randomX = Random.Range(minX, maxX);
        var randomY = Random.Range(minY, maxY);

        m_RectTransform.anchoredPosition = new Vector2(randomX, randomY);
    }
}