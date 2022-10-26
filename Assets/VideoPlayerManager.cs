using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    [SerializeField] RawImage left;
    [SerializeField] RawImage right;

    [SerializeField] AspectRatioFitter leftFitter;
    [SerializeField] AspectRatioFitter rightFitter;

    void Update()
    {
        if (videoPlayer.targetTexture == null)
            videoPlayer.targetTexture = new RenderTexture(0, 0, 24);

        if (videoPlayer.isPlaying)
        {
            if (videoPlayer.width != videoPlayer.targetTexture.width || videoPlayer.height != videoPlayer.targetTexture.height)
            {
                Destroy(videoPlayer.targetTexture);
                videoPlayer.targetTexture = new RenderTexture((int)videoPlayer.width, (int)videoPlayer.height, 24);
            }

            leftFitter.aspectRatio = (float)videoPlayer.width / videoPlayer.height;
            rightFitter.aspectRatio = (float)videoPlayer.width / videoPlayer.height;

            left.texture = videoPlayer.targetTexture;
            right.texture = videoPlayer.targetTexture;
        }
    }
}
