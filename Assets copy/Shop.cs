using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour {
	public bool GunTurretSelected = false;
	public bool GooGunSelected = false;
	public bool SniperSelected = false;
	public Toggle gunTurret;
	public Toggle gooGun;
	public Toggle sniper;
	public static int TurretIndex = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (gunTurret == gunTurret.GetComponent<Toggle>().isOn == false) {
			GunTurretSelected = true;
			GooGunSelected = false;
			SniperSelected = false;
			gooGun.GetComponent<Toggle>().isOn = true;
			sniper.GetComponent<Toggle>().isOn = true;
			TurretIndex = 1;
		}
		if (gooGun.GetComponent<Toggle>().isOn == false) {
			GooGunSelected = true;
			GunTurretSelected = false;
			SniperSelected = false;
			gunTurret.GetComponent<Toggle>().isOn = true;
			sniper.GetComponent<Toggle>().isOn = true;
			TurretIndex = 2;
		}
		if (sniper.GetComponent<Toggle>().isOn == false) {
			SniperSelected = true;
			GunTurretSelected = false;
			GooGunSelected = false;
			gunTurret.GetComponent<Toggle>().isOn = true;
			gooGun.GetComponent<Toggle>().isOn = true;
			TurretIndex = 3;
		}
	}
}
