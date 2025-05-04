using UnityEngine;
using uWindowCapture;

public class CanvasController : MonoBehaviour {
    [field: Header("References")]
    [field: SerializeField] public UwcWindowTexture WindowCapture { get; private set; }
    [field: SerializeField] public Canvas Canvas { get; private set; }
    
    [field: Header("Runtime")]
    [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }
    
    private void Awake() {
        if (CanvasGroup == null && Canvas.TryGetComponent(out CanvasGroup canvasGrp)) {
            CanvasGroup = canvasGrp;
        }
    }
    
    private void Update() {
        if (WindowCapture.window == null) return;
        
        CanvasGroup.alpha = !WindowCapture.window.isMinimized ? 1 : 0;
    }
}