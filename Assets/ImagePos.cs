using UnityEngine;
using UnityEngine.UI;

public class ImagePos : MonoBehaviour
{
    [SerializeField] RectTransform left;
    [SerializeField] RectTransform right;
    [SerializeField] Slider slider;

    void Update()
    {
        left.anchoredPosition = new Vector2(-slider.value * left.rect.width, 0);
        right.anchoredPosition = new Vector2(slider.value * right.rect.width, 0);
    }
}
