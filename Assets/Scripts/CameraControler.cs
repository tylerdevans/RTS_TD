using UnityEngine;

public class CameraControler : MonoBehaviour {
	public float panSpeed = 30f;
	public float panBorderThickness = 10f;
	public float ScrollSpeed = 15;
	public Vector2 zoomRange = new Vector2( -10, 100 );
	public float ScrollEdge = 0.1f;
	public float CurrentZoom = 0;
	public float ZoomZpeed = 1;
	public float ZoomRotation = 1;
	public Vector2 zoomAngleRange = new Vector2(20, 70);
	public float rotateSpeed = 10;

	private Vector3 initialPosition;
	private Vector3 initialRotation;
	private bool doMovement = true;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;      
		initialRotation = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			doMovement = !doMovement;
		if (!doMovement)
			return;
		//Pan
		if (Input.GetKey ("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) 
		{
			transform.Translate (Vector3.forward*panSpeed*Time.deltaTime);
		}
		if (Input.GetKey ("s") || Input.mousePosition.y <= panBorderThickness) 
		{
			transform.Translate (Vector3.back*panSpeed*Time.deltaTime);
		}
		if (Input.GetKey ("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) 
		{
			transform.Translate (Vector3.right*panSpeed*Time.deltaTime);
		}
		if (Input.GetKey ("a") || Input.mousePosition.x <= panBorderThickness) 
		{
			transform.Translate (Vector3.left*panSpeed*Time.deltaTime);
		}

		//Rotate

		if ( Input.GetKey("q") || Input.mousePosition.x <= Screen.width * ScrollEdge ) {
			transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed, Space.World);
		}
		else if ( Input.GetKey("e") || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge) ) {
			transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
		}

		//Zoom
		CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * ZoomZpeed;

		CurrentZoom = Mathf.Clamp( CurrentZoom, zoomRange.x, zoomRange.y );

		transform.position = new Vector3( transform.position.x, transform.position.y - (transform.position.y - (initialPosition.y + CurrentZoom)) * 0.1f, transform.position.z );

		float x = transform.eulerAngles.x - (transform.eulerAngles.x - (initialRotation.x + CurrentZoom * ZoomRotation)) * 0.1f;
		x = Mathf.Clamp( x, zoomAngleRange.x, zoomAngleRange.y );

		transform.eulerAngles = new Vector3( x, transform.eulerAngles.y, transform.eulerAngles.z );
	}
}
