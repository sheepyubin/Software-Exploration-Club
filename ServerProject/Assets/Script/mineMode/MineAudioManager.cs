using UnityEngine;

public class MineAudioManager : MonoBehaviour
{
    // 정적 인스턴스 필드
    public static MineAudioManager instance;

    // BGM 관련 필드
    public AudioClip bgmClip;
    public float bgmVolume;
    private AudioSource bgmPlayer;

    // 효과음 관련 필드
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    private AudioSource[] sfxPlayers;
    private int channelIndex;

    public RoundData roundData;

    // BGM 및 효과음 타입
    public enum Sfx { Rifle, ShotGun, MineExplosion }

    public void Update()
    {
        if (roundData.ReturnClearGame())
        {
            bgmPlayer.Stop();
            return;
        }
    }
    void Awake()
    {
        // 인스턴스 필드 초기화
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경될 때 오브젝트 파괴 방지
            Init(); // 초기화 메서드 호출
        }
        else
        {
            // 이미 인스턴스가 있는 경우 중복 생성 방지
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayBgm(true); // 씬이 시작될 때 BGM 재생
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            if (!bgmPlayer.isPlaying)
            {
                bgmPlayer.Play();
            }
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (!sfxPlayers[loopIndex].isPlaying)
            {
                channelIndex = loopIndex;
                sfxPlayers[channelIndex].clip = sfxClips[(int)sfx];
                sfxPlayers[channelIndex].Play();
                break;
            }
        }
    }
}
