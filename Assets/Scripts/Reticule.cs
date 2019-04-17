using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour
{
    public Pointer pointer;
    public SpriteRenderer circleRenderer;

    public Sprite openSprite;
    public Sprite closedSprite;

    private Camera camera = null;

    private void Awake()
    {
        pointer.OnPointerUpdate += UpdateSprite;

        camera = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(camera.gameObject.transform);
    }
    private void OnDestroy()
    {
        pointer.OnPointerUpdate -= UpdateSprite;
    }
    private void UpdateSprite(Vector3 point, GameObject hitObject)
    {
        transform.position = point;

        if (hitObject)
            circleRenderer.sprite = closedSprite;
        else
            circleRenderer.sprite = openSprite;
    }
}
