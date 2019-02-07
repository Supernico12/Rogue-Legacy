using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    Transform healthUiParent;
    [SerializeField]
    Transform weaponSlotsParent;

    Image[] slotsImages;
    PlayerFighting playerFighting;
    CharacterStats stats;
    Slider slider;
    TextMeshProUGUI Text;

    void Start()
    {
        stats = PlayerManager.instance.player.GetComponent<CharacterStats>();
        slider = healthUiParent.GetComponentInChildren<Slider>();
        Text = healthUiParent.GetComponentInChildren<TextMeshProUGUI>();
        playerFighting = PlayerManager.instance.player.GetComponent<PlayerFighting>();
        playerFighting.OnWeaponChange += UpdateWeaponsSlot;
        slotsImages = weaponSlotsParent.GetComponentsInChildren<Image>();

    }

    void UpdateHP()
    {
        float health = stats.GetCurrentHealth;
        slider.value = health / stats.GetMaxHealth;
        Text.text = health.ToString() + "/" + stats.GetMaxHealth.ToString();
    }

    void UpdateWeaponsSlot()
    {
        slotsImages[1].sprite = playerFighting.weapon1.sprite;
        slotsImages[4].sprite = playerFighting.weapon2.sprite;
    }

    void Update()
    {
        UpdateHP();
    }

}
