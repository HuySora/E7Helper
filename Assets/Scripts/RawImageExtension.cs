using UnityEngine;
using UnityEngine.UI;

public static class RawImageExtension {
    public static void SetTextureAndScaleByWidth(this RawImage rawImage, Texture2D texture) {
        rawImage.texture = texture;
        rawImage.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            rawImage.rectTransform.rect.width * texture.height / texture.width
        );
    }
}