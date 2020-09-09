using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    bool doneFlashing = true;
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

    public void FlashScreen()
    {
        doneFlashing = false;
        opacity = initialFlashIntensity;
    }
}
