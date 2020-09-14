using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    bool doneFlashing = true;

    [SerializeField]PlayerController player;
    Image image;

    float opacity = 0f;
    [SerializeField, Range(0f, 1f)]
    float flashSpeed;
    [SerializeField, Range(0f, 1f)]
    float initialFlashIntensity = 0.7f;

    float r;
    float g;
    float b;

    private void Awake()
    {
        image = GetComponent<Image>();
        Color color = image.color;
        r = color.r;
        g = color.g;
        b = color.b;
    }

    private void Start()
    {
        player.OnDamage.AddListener(FlashScreen);
    }

    void Update()
    {
        if(!doneFlashing)
        {
            opacity -= flashSpeed * Time.deltaTime;
            if(opacity < 0)
            {
                opacity = 0;
                doneFlashing = true;
            }
            image.color = new Color(r, g, b, opacity);
        }
    }

    void FlashScreen(int damageFromEvent)
    {
        //damageFromEvent is discarded and not needed for this method
        doneFlashing = false;
        opacity = initialFlashIntensity;
    }
}
