using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public GUISkin skin;

	public SpriteRenderer spriteRender;
	public Sprite[] tutorialScenes;
	public Sprite[] combinerSprite;

	private GameObject Al;
	private GameObject Global;

	public GameObject[] pickUpItems;

	private GameObject[] pointers;

	private bool scaleLastImage = false;
	private bool showInstruction = false;
	private bool sceneWindowBool = false;
	private bool[] scriptActive = new bool[]{true, false, false, false, false, false};

	public bool[] userPressKey = new bool[]{false, false};
	public bool playNext = false;
	public bool shake = false;

	public string instructText = "";

	private Rect[] combinerRect;

	private bool loadObject = false;
	// Use this for initialization
	void Start () 
	{
		Camera.main.GetComponent<SmoothFollow> ().toUpdateCamera = false;

		Global = GameObject.Find ("GlobalObject");

		Global.GetComponent<ElementInterfaceScript>().enabled = false;

		Al = Global.GetComponent<globalObject>().player;

		Al.GetComponent<HealthScript>().enabled = false;


		combinerRect = new Rect[]{new Rect(new Rect(Screen.width/2 - combinerSprite[0].texture.width/2, 100, combinerSprite[0].texture.width, combinerSprite[0].texture.height)),
			new Rect(new Rect(Screen.width/4 - combinerSprite[1].texture.width/2, 300, combinerSprite[1].texture.width, combinerSprite[1].texture.height)),
			new Rect(new Rect(Screen.width - Screen.width/4 - combinerSprite[2].texture.width/2, 300, combinerSprite[2].texture.width, combinerSprite[2].texture.height))};
	
		pointers = new GameObject[pickUpItems.Length];
	}
	
	// Update is called once per frame
	void Update () {
		//Beginning Scene
		//can probably use a switch case to do this but for now just code fast

		if(scriptActive[0])
		{
			playScript.startScript ("cutScene.txt", false);
			nextImage(playScript.textInt);

			if(scaleLastImage)
				scaleLastImage = startImageScale(0.06f);

			if(playScript.textInt == -1 && !scaleLastImage)
			{	
				spriteRender.enabled = false;
				scriptActive[0] = false;
				Camera.main.GetComponent<SmoothFollow> ().toUpdateCamera = true;

				StartCoroutine("pauseDiag", 1);
			}
		}

		if(scriptActive[1])
		{
			playScript.startScript ("lab01.txt", false);

			if(playScript.textInt == -1)
			{	
				showInstruction = true;

				if(!userPressKey[0] || !userPressKey[1])
				{
					instructText = "Walk around using the '"+ KeyList.keyLeft.ToString() +"' and '" + KeyList.keyRight.ToString() + "' keys";

					if(Input.GetKey(KeyList.keyRight))
						userPressKey[0] = true;

					if(Input.GetKey(KeyList.keyLeft))
						userPressKey[1] = true;
				
					if(!loadObject)
					{	
						loadObject = true;
						
						Vector3 pos = pickUpItems[0].transform.position;
						pointers[0] = (GameObject)Instantiate(Resources.Load("_Prefab/PointerPrefab", typeof(GameObject)), new Vector3(pos.x, pos.y + 0.19f, pos.z), Quaternion.identity);
						
						pos = pickUpItems[1].transform.position;
						pointers[1] = (GameObject)Instantiate(Resources.Load("_Prefab/PointerPrefab", typeof(GameObject)), new Vector3(pos.x, pos.y + 0.19f, pos.z), Quaternion.identity);
					}
				}
				if(userPressKey[0] && userPressKey[1])
				{	
					instructText = "Use '" + KeyList.keyInteract.ToString() +"' to pick up the different pieces of the Chemical Bond Sequencer located around the room.";


					AlPickUpFunc(pickUpItems[0], pointers[0]);
					AlPickUpFunc(pickUpItems[1], pointers[1]);

					if(!pickUpItems[0].activeSelf && !pickUpItems[1].activeSelf)
						playNext = true;
				}


				if(playNext)
				{
					loadObject = false;
					showInstruction = false;
					StartCoroutine("pauseDiag", 2);
				}
			}
		}

		if(scriptActive[2])
		{
			playScript.startScript ("lab03.txt", false);

			if(!loadObject)
			{
				loadObject = true;

				Vector3 pos = pickUpItems[2].transform.position;
				pointers[2] = (GameObject)Instantiate(Resources.Load("_Prefab/PointerPrefab", typeof(GameObject)), new Vector3(pos.x, pos.y + 0.19f, pos.z), Quaternion.identity);
			}

			if(playScript.textInt == -1)
			{
				if(Al.renderer.bounds.Intersects(pickUpItems[2].renderer.bounds))
				{
					if(Input.GetKeyDown(KeyList.keyInteract))
					{	
						pointers[2].SetActive(false);
						sceneWindowBool = true;
					}
				}
			}
		}

		if(scriptActive[3])
		{
			playScript.startScript ("lab04.txt", false);

			if(playScript.textInt == -1)
			{
				showInstruction = true;
				instructText = "Press '" + KeyList.keyJump.ToString() + "' to jump. Jump and grab the oxygen and hydrogen elements on the shelf!";
			
				if(!loadObject)
				{
					loadObject = true;

					for(int i = 3; i < pickUpItems.Length; i++)
					{
						Vector3 pos = pickUpItems[i].transform.position;
						pointers[i] = (GameObject)Instantiate(Resources.Load("_Prefab/PointerPrefab", typeof(GameObject)), new Vector3(pos.x, pos.y + 0.19f, pos.z), Quaternion.identity);
					}
				}

				for(int i = 3; i < pickUpItems.Length; i++)
				{	
					if(!pickUpItems[i].activeSelf)
						pointers[i].SetActive(false);
				}

				if(Global.GetComponent<ElementInterfaceScript>().ID.inventoryData[2,0]._amount > 9 &&
				   Global.GetComponent<ElementInterfaceScript>().ID.inventoryData[1,0]._amount > 9 )
				{
					scriptActive[3] = false;
					showInstruction = false;
					StartCoroutine("pauseDiag", 4);

					for(int i = 3; i < pointers.Length; i++)
					{
						pointers[i].SetActive(false);
					}
				}

			}
			else
				loadObject = false;
		}

		if(scriptActive[4] && Al.GetComponent<AlScript>()._groundState)
		{
			playScript.startScript ("lab05.txt", false);
			if(playScript.textInt == -1)	
				shake = true;

			if(shake)
				shakeFunc();
		}

		if(scriptActive[5])
		{
			playScript.startScript ("lab06.txt", false);

			if(playScript.textInt == -1)	
			{
				showInstruction = true;
				instructText = "Help Al find out what happen! Leave the lab and investigate.";
			}
		}
	}

	private void AlPickUpFunc(GameObject go, GameObject point)
	{
		if(Al.renderer.bounds.Intersects(go.renderer.bounds))
		{
			if(Input.GetKeyDown(KeyList.keyInteract))
			{	
				go.SetActive(false);
				point.SetActive(false);
			}
		}
	}

	private float shakeSize = 3.0f;
	private float shakeAmount = 0.5f;
	private float decreaseFactor = 1.0f;

	private void shakeFunc()
	{
		if (shakeSize > 0) {
			Camera.main.GetComponent<SmoothFollow> ().toUpdateCamera = false;
			Vector3 shakeShake = Random.insideUnitSphere * shakeAmount;
			Camera.main.transform.localPosition = new Vector3(shakeShake.x, shakeShake.y, Camera.main.transform.localPosition.z);
			shakeSize -= Time.deltaTime * decreaseFactor;
			
		} else {
			Camera.main.GetComponent<SmoothFollow> ().toUpdateCamera = true;
			shakeSize = 0.0f;

			StartCoroutine("pauseDiag", 5);
		}
	}

	private IEnumerator pauseDiag(int nextScene)
	{
		yield return new WaitForSeconds(0.5f);
		playScript.loadScript = false;
		scriptActive[nextScene - 1] = false;
		scriptActive[nextScene] = true;
	}

	private bool startImageScale(float finScale)
	{
		float scaleX = 0;
		float scaleY = 0;

		scaleX = Mathf.Lerp(gameObject.transform.localScale.x, finScale, Time.deltaTime/1.2f);
		scaleY = Mathf.Lerp(gameObject.transform.localScale.y, finScale, Time.deltaTime/1.2f);

		gameObject.transform.localScale = new Vector3(scaleX, scaleY, 1);

		if(gameObject.transform.localScale.x > 0.061f)
			return true;
		else
			return false;
	}

	void nextImage(int diagInt)
	{
		switch(diagInt)
		{
		case 0:
			spriteRender.sprite = tutorialScenes[0];
			break;
		case 1:
			spriteRender.sprite = tutorialScenes[1];
			break;
		case 2:
			spriteRender.sprite = tutorialScenes[2];
			break;
		case 3:
			spriteRender.sprite = tutorialScenes[3];
			break;
		case 4:
			spriteRender.sprite = tutorialScenes[3];
			scaleLastImage = true;
			break;
		}
	}

	void OnGUI()
	{
		GUI.skin = skin;

		if(showInstruction)
			GUI.Window(22, new Rect(Screen.width - 218 - 6, 6, 218, 85), InstructWindow, "", "instructBox");

		if(sceneWindowBool)
			GUI.Window(23, new Rect(0, 0, Screen.width, Screen.height), sceneWindow, "", "instructOverlay");
	}
	
	public void sceneWindow(int winID)
	{
		for(int i = 0; i < combinerSprite.Length; i++)
		{
			GUI.Box(combinerRect[i], combinerSprite[i].texture, "");

			combinerRect[i].x = Mathf.Lerp(combinerRect[i].x, Screen.width/2, Time.deltaTime/2);
			combinerRect[i].y = Mathf.Lerp(combinerRect[i].y, Screen.height/2, Time.deltaTime/2);
		}

		if(combinerRect[0].x > (Screen.width/2 - 0.50f))
		{	
			sceneWindowBool = false;
			StartCoroutine("pauseDiag", 3);
		}
	}

	public void InstructWindow(int widID)
	{
		if(showInstruction)
			GUI.Box(new Rect(10, 10, 200, 47), instructText, "instructFont"); 
	}
}
