using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {
	//[SerializeField] private SpriteRenderer[] graffiti_spriteRend = null;
	//private Material[] graffiti_mat;
	[SerializeField] private GameObject directionalLight = null;
	[SerializeField] private SpriteRenderer[] walls = null;
	private Sprite[] wallSprite;
	public bool isLightOn;
	[SerializeField] private Material graffiti_mat = null;
	private Color alpha_zero, alpha_one, alpha_increment, alpha_decrement, white, grey;

	private void Awake(){
		directionalLight.SetActive (true);
		isLightOn = true;
		alpha_one = Color.white;
		alpha_zero = new Color (1, 1, 1, 0);
		alpha_increment = new Color (0, 0, 0, Time.deltaTime);
		alpha_decrement = new Color (0, 0, 0, -Time.deltaTime);
		white = Color.white;
		grey = Color.gray;
		graffiti_mat.color = alpha_zero;
		foreach (SpriteRenderer wall in walls) {
			wall.color = white;
		}
	}

	public void ToggleLight(){
		isLightOn = !isLightOn;
		directionalLight.SetActive (isLightOn);
		if (isLightOn) {
			foreach (SpriteRenderer wall in walls) {
				wall.color = white;
			}
			StartCoroutine (SwitchOnLight());
		} else {
			foreach (SpriteRenderer wall in walls) {
				wall.color = grey;
			}
			StartCoroutine (SwitchOffLight());
		}
	}

	private IEnumerator SwitchOnLight(){
		while (graffiti_mat.color.a > 0 && isLightOn) {
			graffiti_mat.color = graffiti_mat.color + alpha_decrement;
			yield return null;
		}
		yield return graffiti_mat.color = alpha_zero;
	}

	private IEnumerator SwitchOffLight(){
		while (graffiti_mat.color.a < 1 && !isLightOn) {
			graffiti_mat.color = graffiti_mat.color + alpha_increment;
			yield return null;
		}
		yield return graffiti_mat.color = alpha_one;
	}
	/*
	private void Awake(){
		directionalLight.SetActive (true);
		isLightOn = true;
		graffiti_mat = new Material[graffiti_spriteRend.Length];
		for (int i = 0; i < graffiti_spriteRend.Length; i++){
			graffiti_mat [i] = graffiti_spriteRend [i].material;
		}
	}

	public void ToggleLight(){
		isLightOn = !isLightOn;
		directionalLight.SetActive (isLightOn);
		if (isLightOn) {
			StartCoroutine (SwitchOnLight());
		} else {
			StartCoroutine (SwitchOffLight());
		}
	}

	private IEnumerator SwitchOnLight(){
		float colorValue = 1;
		while (colorValue > 0 && isLightOn) {
			foreach (Material mat in graffiti_mat) {
				mat.color = mat.color - new Color (0, 0, 0, 0.1f);
				colorValue = mat.color.a;
				yield return null;
			}
		}
		yield return null;
	}

	private IEnumerator SwitchOffLight(){
		print ("hello");
		float colorValue = 0;
		while (colorValue < 1 && !isLightOn) {
			foreach (Material mat in graffiti_mat) {
				mat.color = mat.color + new Color (0, 0, 0, 0.1f);
				colorValue = mat.color.a;
				yield return null;
			}
		}
		yield return null;
	}
	*/
}
