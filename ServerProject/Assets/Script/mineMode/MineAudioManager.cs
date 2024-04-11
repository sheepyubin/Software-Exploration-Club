using UnityEngine;

public class MineAudioManager : MonoBehaviour
{
    // ���� �ν��Ͻ� �ʵ�
    public static MineAudioManager instance;

    // BGM ���� �ʵ�
    public AudioClip bgmClip;
    public float bgmVolume;
    private AudioSource bgmPlayer;

    // ȿ���� ���� �ʵ�
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    private AudioSource[] sfxPlayers;
    private int channelIndex;

    public RoundData roundData;

    // BGM �� ȿ���� Ÿ��
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
        // �ν��Ͻ� �ʵ� �ʱ�ȭ
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� ����� �� ������Ʈ �ı� ����
            Init(); // �ʱ�ȭ �޼��� ȣ��
        }
        else
        {
            // �̹� �ν��Ͻ��� �ִ� ��� �ߺ� ���� ����
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayBgm(true); // ���� ���۵� �� BGM ���
    }

    void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // ȿ���� �÷��̾� �ʱ�ȭ
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
