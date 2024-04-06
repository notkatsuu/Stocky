using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumeItem : MonoBehaviour, IConsume
{
    public delegate void HealPlayer(int value);
    public static event HealPlayer OnHealPlayer;
    [SerializeField] private Slider healthBar;

    public void Use(ConsumableItem item)
    {
        Debug.Log("Healed");
        OnHealPlayer?.Invoke(item.LifeRestore);
    }
}