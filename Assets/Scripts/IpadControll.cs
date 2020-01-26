using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;


public class IpadControll : MonoBehaviour
{
    [SerializeField] List<Texture2D> textures;
    [SerializeField] Material Screen;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] List<FileInfo> videoClipsUrl;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.enabled = false;
        index = 0;
        FillImageList();
        FillVideoArray();
        Screen.SetTexture("_BaseColorMap", textures[index]);
    }


    void FillImageList()
    {
        string currentFolderPath = System.Environment.CurrentDirectory;
        DirectoryInfo d = new DirectoryInfo(currentFolderPath + "/IpadMedia/" + "/images/");
        FileInfo[] files = d.GetFiles("*.png");
        foreach (FileInfo fileInfo in files)
        {
            textures.Add(LoadPNG(fileInfo));
        }
    }

    static Texture2D LoadPNG(FileInfo filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath.FullName))
        {
            fileData = File.ReadAllBytes(filePath.FullName);
            tex = new Texture2D(2, 2);
            tex.name = Path.GetFileNameWithoutExtension(filePath.Name);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    void FillVideoArray()
    {
        string currentFolderPath = System.Environment.CurrentDirectory;
        DirectoryInfo d = new DirectoryInfo(currentFolderPath + "/IpadMedia/" + "/video/");
        FileInfo[] files = d.GetFiles("*.mp4");
        //videoClipsUrl.AddRange(d.GetFiles("*.mp4"));
        videoClipsUrl.AddRange(files);
    }



    public void NextFile()
    {
        index++;
        foreach(Texture texture in textures)
        {
            if(texture.name == index.ToString())
            {
                Screen.SetTexture("_BaseColorMap", texture);
            }
        }

        if (Screen.GetTexture("_BaseColorMap").name != index.ToString())
        {
            foreach (FileInfo fileInfo in videoClipsUrl)
            {
                if (fileInfo.Name == index.ToString())
                {
                    videoPlayer.enabled = true;
                    LoadVideo(fileInfo.FullName.Replace("\\", "/"));
                }
            }
        }
    }

    public void LoadVideo(string Url)
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = Url;
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += VideoPlayer_prepareCompleted;
    }

    private void VideoPlayer_prepareCompleted(VideoPlayer source)
    {
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            NextFile();
        }
    }
}
