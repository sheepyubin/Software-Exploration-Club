using UnityEngine;
using UnityEngine.UI;

public class mine_ui : MonoBehaviour
{
    public Sprite[] ImgAsset;
    private Image NowImage;
    public RoundData roundData;

    float time;
    public float _fadeTime = 5f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        int img = roundData.ReturnRound() - 1;
        NowImage = GetComponent<Image>();

        NowImage.sprite = ImgAsset[img];

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < _fadeTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / _fadeTime);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }
        else
        {
            time = 0;
            gameObject.SetActive(false);
        }
        time += Time.deltaTime;
    }

    public void resetAnim()
    {
        spriteRenderer.color = originalColor;
        gameObject.SetActive(true);
    }
}
