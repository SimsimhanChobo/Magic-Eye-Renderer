using B83.Win32;
using SFB;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Manager : MonoBehaviour
{
    static readonly string[] textureExtension = new string[] { "tga", "targa", "dds", "png", "jpg", "jif", "jpeg", "jpe", "bmp", "exr", "gif", "hdr", "iff", "pict", "tif", "tiff", "psd", "ico", "jng", "koa", "lbm", "mng", "pbm", "pcd", "pcx", "pgm", "ppm", "ras", "wbpm", "cut", "xbm", "xpm", "g3", "sgi", "j2k", "j2c", "jp2", "pfm", "webp", "jxr" };
    static readonly string[] videoExtension = new string[] { "asf", "avi", "dv", "m4v", "mov", "mp4", "mpg", "mpeg", "ogv", "vp8", "webm", "wmv" };
    static readonly string[] allExtension = new string[] { "tga", "targa", "dds", "png", "jpg", "jif", "jpeg", "jpe", "bmp", "exr", "gif", "hdr", "iff", "pict", "tif", "tiff", "psd", "ico", "jng", "koa", "lbm", "mng", "pbm", "pcd", "pcx", "pgm", "ppm", "ras", "wbpm", "cut", "xbm", "xpm", "g3", "sgi", "j2k", "j2c", "jp2", "pfm", "webp", "jxr", "asf", "avi", "dv", "m4v", "mov", "mp4", "mpg", "mpeg", "ogv", "vp8", "webm", "wmv" };
    static readonly ExtensionFilter[] extensionFilters = new ExtensionFilter[] { new ExtensionFilter("Image Files ", allExtension)};

    [SerializeField] VideoPlayer videoPlayer;

    [SerializeField] RawImage left;
    [SerializeField] RawImage right;

    [SerializeField] AspectRatioFitter leftFitter;
    [SerializeField] AspectRatioFitter rightFitter;

    void OnEnable()
    {
        UnityDragAndDropHook.InstallHook();
        UnityDragAndDropHook.OnDroppedFiles += RefreshDragAndDrop;
    }

    void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
        UnityDragAndDropHook.OnDroppedFiles -= RefreshDragAndDrop;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            string[] paths = StandaloneFileBrowser.OpenFilePanel("Open", "", extensionFilters, false);
            if (paths.Length > 0)
                Refresh(paths[0]);
        }
    }

    void RefreshDragAndDrop(List<string> paths, POINT point) => Refresh(paths[0]);


    Texture2D lastTexture;
    void Refresh(string path)
    {
        if (lastTexture != null)
            Destroy(lastTexture);

        videoPlayer.Stop();

        for (int i = 0; i < videoExtension.Length; i++)
        {
            if (Path.GetExtension(path) == "." + videoExtension[i])
            {
                videoPlayer.url = "file://" + path;
                videoPlayer.Play();

                return;
            }
        }

        for (int i = 0; i < textureExtension.Length; i++)
        {
            if (Path.GetExtension(path) == "." + textureExtension[i])
            {
                Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                AsyncImageLoader.LoadImage(texture, File.ReadAllBytes(path));
                texture.filterMode = FilterMode.Point;

                left.texture = texture;
                right.texture = texture;

                leftFitter.aspectRatio = (float)texture.width / texture.height;
                rightFitter.aspectRatio = (float)texture.width / texture.height;

                lastTexture = texture;
            }
        }
    }
}
