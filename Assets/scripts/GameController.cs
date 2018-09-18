using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using NAudio;
using NAudio.Wave;
//using System;

public class GameController : MonoBehaviour
{
    public Camera cam;
    public Canvas can;
    public GameObject beat;
    public static int score;
    public string songMapPath;
    public string songPath;
    public float BPS; //how many beats fall from the sky per second.
    [SerializeField]
    public AudioSource audioTrack;
    [SerializeField]
    public AudioSource audioPlay;
    public float[] spectrum = new float[1024];
    public float[] spectrumOld = new float[1024];
    public float largestSoundValue = 0.0f;
    public int largestSample = 0;
    //public string url;
    public FFTWindow FFTMode;
    public int sampleCount;
    public Transform[] cubies;
    float[] samples;
    //public AudioSource source;
    bool isReady = false;
    public GameObject enemy1;
    [SerializeField]
    public static float enemySpawnAngle;
    public Vector3 rotationVector;
    public static bool songStarted=false;
    public int trackDecide;
    public int trackDecideLast;

    [SerializeField]
    private string[] songMap;
    private Vector3 spawnPosition;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private int arrayPos=0;
    [SerializeField]
    private AudioClip song;
    [SerializeField]
    string[] split;
    [SerializeField]
    private int enemySpawnCounter=0;
    private Vector3 enemySpawPosition;
    private Quaternion enemySpawnRotation;


    //Initialization
    void Start()
    {
        trackDecide = 3;
        trackDecideLast = 8;
        split = MenuControl.songFilePath.Split(new char[] { '.' });
        string ext = split[split.Length - 1];  //extension of music file
        if(ext=="mp3" || ext== "ogg" || ext=="wav")
        {
            songPath = MenuControl.songFilePath;
        }
        else
        {
            songPath = Application.dataPath + "/resources/songMaps/[muse] START;DASH/song.ogg";
            songPath = songPath.Remove(0,2);
            songPath = songPath.Replace("/", @"\");
            Debug.Log(songPath);
            //songPath = @"\Users\Skyler\Documents\Unity Projects\Subrosu Knights\Assets\resources\songMaps\[muse] START;DASH\song.ogg";
            //Debug.Log(songPath);
        }
        //songMap = new string[] { "1", "2", "5" };
        //if(cam==null)
        //{
        //    cam = Camera.main;
        //}
        //Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        //Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        //WWW www = new WWW(url);
        //song = www.audioClip;
        //audioTrack = GetComponent<AudioSource>();
        ReadSongMap();
        StartSongBackground();
        score = 0;
        StartCoroutine(SpawnBeat());
        StartCoroutine(MusicStart());
        StartCoroutine(StartSong());
        UpdateScore();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < spectrum.Length; i++)
        {
            spectrumOld[i] = spectrum[i];
        }
        UpdateScore();
        audioTrack.GetOutputData(spectrum,0);
    }

    void UpdateScore()
    {
        scoreText.text = "" + score;
    }

    void ReadSongMap()
    {
        StreamReader reader = new StreamReader(Application.dataPath+songMapPath);
        string s = reader.ReadLine();
        //string r = "1:2:3:2:4:7:1";
        if (s != null)
        {
            songMap = s.Split(':');
        }
    }

    void StartSongBackground()
    {
        audioTrack.PlayOneShot(song, 1.0f);
    }

    IEnumerator StartSong()
    {
        yield return new WaitForSeconds(0.59f);
        songStarted = true;
        audioPlay.PlayOneShot(song, 1.0f);
    }

    IEnumerator SpawnBeat()
    {
        //yield return new WaitForSeconds(1);
        while (true) 
        {
            //int trackDecide = Random.Range(1, 5); //with ints, Random.Range is inclusive, so it won't produce 5.
            //int trackDecide = 1;
            //int trackDecide = Convert.ToInt32(songMap[arrayPos]);
            /*
            for(int i=0; i<spectrum.Length/4; i++) //so hacky, i cry evertiem
            {
                track1 += spectrum[i+0];
                track2 += spectrum[i+256];
                track3 += spectrum[i+512];
                track4 += spectrum[i+768];
            }
            */
            largestSample = 0;
            largestSoundValue=0.0f;
            float[] spectrumHolder = spectrum;
            for (int i = 0; i < spectrum.Length; i++)
            {
                if (Mathf.Abs(spectrum[i]-spectrumOld[i]) > largestSoundValue)
                {
                    largestSoundValue = spectrum[i] - spectrumOld[i];
                    largestSample = i;
                }
            }
            if (largestSoundValue>0)
            {
                switch (largestSample % 4)
                {
                    case 0:
                        trackDecide = 1;
                        break;

                    case 1:
                        trackDecide = 2;
                        break;

                    case 2:
                        trackDecide = 3;
                        break;

                    case 3:
                        trackDecide = 4;
                        break;
                }
            }
            else
            {
                trackDecide = trackDecideLast;
            }
            /*
            for (int i=0;i<spectrum.Length;i++)
            {
                i>=0&&i<=63
                i>=64&&i<=127
                i>=128&&i<=191
                i>=192&&i<=255
                i>=256&&i<=319
                i>=320&&i<=383
                i>=384&&i<=447
                i>=448&&i<=511
                i>=512&&i<=575
                i>=576&&i<=639
                i>=640&&i<=703
                i>=704&&i<=767
                i>=768&&i<=831
                i>=832&&i<=895
                i>=896&&i<=959
                i>=960&&i<=1023
            }
            */
            //trackDecide = 1;
            switch(trackDecide)
            {
                case 8://no track
                    spawnPosition = new Vector3((Screen.width * -0.29036463542f),
                        (Screen.height * 1.1f),
                        0.0f);
                    trackDecideLast = 8;
                    break;
                case 1: //track 1
                    spawnPosition = new Vector3((Screen.width * 0.0598046875f), 
                        (Screen.height * 1.1f), 
                        0.0f);
                    trackDecideLast = 1;
                    break;
                case 2: //track 2
                    spawnPosition = new Vector3((Screen.width * 0.17494796875f),
                        (Screen.height * 1.1f),
                        0.0f);
                    trackDecideLast = 2;
                    break;
                case 3: //track 3
                    spawnPosition = new Vector3((Screen.width * 0.29036463542f),
                        (Screen.height * 1.1f),
                        0.0f);
                    trackDecideLast = 3;
                    break;
                case 4: //track 4
                    spawnPosition = new Vector3((Screen.width * 0.40536458333f),
                        (Screen.height * 1.1f),
                        0.0f);
                    trackDecideLast = 4;
                    break;
                default:
                    //no beat happens
                    break;
            }
            arrayPos++;
            enemySpawnCounter++;
            if(enemySpawnCounter>4)
            {
                //Quaternion enemySpawnRotation = new Quaternion(0, 0, enemySpawnAngle, 0);
                //enemySpawPosition = new Vector3((Screen.width * 1.1f), 
                //    (Screen.height * 1.1f), 
                //    0.0f);
                //enemySpawPosition = new Vector3((cam.transform.position.x+cam.pixelWidth),
                //    (cam.transform.position.y + cam.pixelHeight),
                //    0.0f);
                enemySpawPosition = RandomCircle(new Vector3(cam.transform.position.x+1.5f,cam.transform.position.y), 5.0f);
                //Quaternion enemySpawnRotation = new Quaternion(0, 0, -enemySpawnAngle, 1);
                //Quaternion enemySpawnRotation = Quaternion.Euler(0, 180, 180 - enemySpawnAngle);
                enemySpawnRotation = Quaternion.Euler(0, 180,enemySpawnAngle);
                GameObject enemy1Clone = Instantiate(enemy1, enemySpawPosition, enemySpawnRotation) as GameObject;
                //float enemySpawnAngleHolder = -enemySpawnAngle;
                //Vector3 rotationVector = enemy1Clone.transform.rotation.eulerAngles;
                //rotationVector.y = -enemySpawnAngle;
                //enemy1Clone.transform.Rotate(rotationVector);
                //enemy1Clone.transform.rotation = Quaternion.LookRotation(cam.transform.position - enemy1Clone.transform.position);
                //enemy1Clone.transform.SetParent(can.transform);
                //rotationVector = enemy1Clone.transform.rotation.eulerAngles;
                //rotationVector.z = 180-rotationVector.z;
                //rotationVector.y = 180;
                //enemy1Clone.transform.Rotate(rotationVector);
                enemy1Clone.transform.SetParent(cam.transform);
                enemySpawnCounter = 0;
                //enemy1Clone.transform.rotation = Quaternion.Euler(0,0,90);
                //Debug.Log(enemy1Clone.transform.rotation);
                enemySpawnRotation = Quaternion.identity;
            }
            Quaternion spawnRotation = Quaternion.identity;
            GameObject beatClone=Instantiate(beat, spawnPosition, spawnRotation) as GameObject;
            beatClone.transform.SetParent(can.transform);
            yield return new WaitForSeconds(BPS);
        }
    }

    IEnumerator MusicStart() //http://forum.unity3d.com/threads/audio-importing-files-at-runtime.140088/
                             //http://forum.unity3d.com/threads/load-sounds-at-runtime.336112/
    {
        split = songPath.Split(new char[] { '.' });
        string ext = split[split.Length-1];  //extension of music file

        if (ext == "mp3") 
        {
            Directory.CreateDirectory(System.IO.Path.GetTempPath() + @"\SubrosuKnights");
            Mp3ToWav(songPath, System.IO.Path.GetTempPath() + @"\SubrosuKnights\currentsong.wav");
            ext = "wav";
        }
        else
        {
            Directory.CreateDirectory(System.IO.Path.GetTempPath() + @"\SubrosuKnights");
            File.WriteAllBytes(System.IO.Path.GetTempPath() + @"\SubrosuKnights\currentsong." + ext, File.ReadAllBytes(songPath));
        }

        WWW www = new WWW("file:///" + System.IO.Path.GetTempPath() + @"\SubrosuKnights\currentsong." + ext);
        //Debug.Log(www.url);
        song = www.GetAudioClip(true,false);

        song = www.audioClip;
        while (!song.isReadyToPlay)
        {
            Debug.Log("still in loop");
            yield return www;
        }
        song = www.audioClip;
        StartSongBackground();
    }

    public static void Mp3ToWav(string mp3File, string outputFile)
    {
        using (Mp3FileReader reader = new Mp3FileReader(mp3File))
        {
            WaveFileWriter.CreateWaveFile(outputFile, reader);
        }
    }

    Vector3 RandomCircle(Vector3 center,float radius)
    {
        enemySpawnAngle = UnityEngine.Random.Range(0.125f, 0.375f)*360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(enemySpawnAngle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(enemySpawnAngle * Mathf.Deg2Rad);
        pos.z = 0.0f;
        return pos;
    }
}
