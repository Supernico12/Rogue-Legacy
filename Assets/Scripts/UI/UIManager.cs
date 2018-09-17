using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

	[SerializeField]
	Transform healthUiParent;
	
	CharacterStats stats;
	Slider slider;
	TextMeshProUGUI Text;

	void Start(){
		stats = PlayerManager.instance.player.GetComponent<CharacterStats>();
		slider = healthUiParent.GetComponentInChildren<Slider>();
		Text = healthUiParent.GetComponentInChildren<TextMeshProUGUI>();
	}

	void UpdateHP(){
		float health = stats.GetCurrentHealth;
		slider.value = health / stats.GetMaxHealth;
		Text.text = health.ToString() + "/" +  stats.GetMaxHealth.ToString();
	}

	void Update(){
		UpdateHP();
	}

}
