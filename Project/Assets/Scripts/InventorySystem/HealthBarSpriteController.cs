using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSpriteController : MonoBehaviour
{
    private void Awake()
    {
        HealthController.OnUpdateSprite += UpdateSprite;
    }

    private void UpdateSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }

    private void OnDestroy()
    {
        HealthController.OnUpdateSprite -= UpdateSprite;
    }
}
