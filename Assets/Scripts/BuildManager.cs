using UnityEngine;
using UnityEngine.UI;
public class BuildManager : MonoBehaviour {

	public static BuildManager instance;
	public GameObject ShopOBJ;
	public GameObject GunTurret;
	public GameObject GooGun;
	public GameObject Sniper;
	public static bool TurretSelected = false;
	private GameObject turretSelected;
	public static int Points = 100;
	public Text PointCount;
	public static int Cost;
	public int gunTurretCost = 50;
	public int gooGunCost = 25;
	public int SniperCost = 100;
	void Awake ()
	{
		if (instance != null) {
			Debug.LogError ("More than one BuildManager in scene");
			return;
		}
		instance = this;
	}
		
	void Update ()
	{
		PointCount.text = ("Points: ") + (Points).ToString();
		if (Shop.TurretIndex == 0) {
			TurretSelected = false;
		}
		if (Shop.TurretIndex == 1) {
			turretToBuild = GunTurret;
			TurretSelected = true;
			Cost = gunTurretCost;
		}
		if (Shop.TurretIndex == 2) {
			turretToBuild = GooGun;
			TurretSelected = true;
			Cost = gooGunCost;
		}
		if (Shop.TurretIndex == 3) {
			turretToBuild = Sniper;
			TurretSelected = true;
			Cost = SniperCost;
		}
	} 
	private GameObject turretToBuild;

	public GameObject GetTurretToBuild ()
	{
		return turretToBuild;
	}
}
