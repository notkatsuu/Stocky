using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public delegate void UpdateSprite(Sprite sprite);
    public static event UpdateSprite OnUpdateSprite;
    [SerializeField] private Slider healthBar;
    [SerializeField] private List<Sprite> heartSprites = new List<Sprite>();

    private void Awake()
    {
        ConsumeItem.OnHealPlayer += ChangeHealth;
    }
    private void Start()
    {
        heartSprites.AddRange(Resources.LoadAll<Sprite>("Sprites/UI/HealthBar"));
        ChangeSprite();
    }
    public void ChangeHealth(int ammount)
    {
        healthBar.value += ammount;
        if (healthBar.value <= 0)
        {
            SceneController.Instance.NextScene();
        }
        else
        {
            ChangeSprite();
        }
    }
    private void ChangeSprite()
    {
        int index = Mathf.FloorToInt((healthBar.maxValue-healthBar.value) / (healthBar.maxValue/heartSprites.Count));
        OnUpdateSprite?.Invoke(heartSprites[index]);
    }

    private void OnDestroy()
    {
        ConsumeItem.OnHealPlayer -= ChangeHealth;
    }
}
