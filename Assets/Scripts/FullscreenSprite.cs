using UnityEngine;

public class FullscreenSprite : MonoBehaviour
{
    void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // transform.position = Camera.main.transform.position + Vector3.forward * this.transform.position.z;

        transform.localScale = new Vector3(1, 1, 1);
        Vector3 lossyScale = transform.lossyScale;

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        transform.localScale = xWidth;
        //transform.localScale.x = worldScreenWidth / width;
        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight;

        Vector3 newLocalScale = new Vector3(transform.localScale.x / lossyScale.x,
            transform.localScale.y / lossyScale.y, 1f
        //transform.localScale.z / lossyScale.z
        );

        transform.localScale = newLocalScale;

    }

    //void Awake()  // old code but it works but only in gameScreens 
    //{
    //    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

    //    float cameraHeight = Camera.main.orthographicSize * 2;
    //    Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
    //    Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

    //    Vector2 scale = transform.localScale;
    //    if (cameraSize.x >= cameraSize.y)
    //    { // Landscape (or equal)
    //        scale *= cameraSize.x / spriteSize.x;
    //    }
    //    else
    //    { // Portrait
    //        scale *= cameraSize.y / spriteSize.y;
    //    }

    //    transform.position = Vector2.zero; // Optional
    //    transform.localScale = scale;
    //}
}
