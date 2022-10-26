using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoVolume : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Slider slider;

    void Update() => videoPlayer.SetDirectAudioVolume(0, slider.value);
}
