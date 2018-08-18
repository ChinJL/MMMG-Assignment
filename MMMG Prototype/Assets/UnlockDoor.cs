using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour {
	[SerializeField] private GameObject secretDoor_col = null;
	[SerializeField] private MeshRenderer laserSensor1_MeshRend = null, laserSensor2_MeshRend = null;
	private Color lightBlue;
	private bool laserSwitchOn;
	private Color alpha_zero;

	private LightSwitch lightSwitch;
	[SerializeField] private SpriteRenderer secretDoor_mat = null;
	private Color alpha_increment;
	private SwitchRoom switchRoom;

	private void Awake(){
		lightBlue = new Color (0, 0.5f, 1);
		alpha_zero = new Color (1, 1, 1, 0);
		secretDoor_mat.color = alpha_zero;
		alpha_increment = new Color (0, 0, 0, Time.deltaTime /2);
		lightSwitch = GetComponent<LightSwitch> ();
		secretDoor_col.SetActive (false);
		switchRoom = GetComponent<SwitchRoom> ();
	}

	private void Update(){
		//add one more condition, room in right place? or bool for 2 doors' collider collide
		if (laserSensor1_MeshRend.material.color == lightBlue && laserSensor2_MeshRend.material.color == lightBlue) {
			laserSwitchOn = true;
		}
		else {
			laserSwitchOn = false;
		}

		if(laserSwitchOn && lightSwitch.isLightOn && switchRoom.isArranged && !switchRoom.isSwitching) {
			secretDoor_mat.color = secretDoor_mat.color + alpha_increment;
			if (secretDoor_mat.color == Color.white)
				secretDoor_col.SetActive (true);
			//color increase, enable collider
		} else {
			secretDoor_mat.color = alpha_zero;
			secretDoor_col.SetActive (false);
		}
	}
}
