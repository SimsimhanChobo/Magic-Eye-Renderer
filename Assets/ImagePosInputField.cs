using UnityEngine;
using UnityEngine.UI;

public class ImagePosInputField : MonoBehaviour
{
    [SerializeField] InputField field;
    [SerializeField] Slider slider;

    void Update() => field.text = slider.value.ToString();

    public void OnEndEdit(string value)
    {
        if (float.TryParse(value, out float result))
            slider.value = result;
    }
}
