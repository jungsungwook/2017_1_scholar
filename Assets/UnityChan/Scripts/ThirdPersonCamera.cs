//
// Unityちゃん用の三人称カメラ
// 
// 2013/06/07 N.Kobyasahi
//
using UnityEngine;
using System.Collections;


public class ThirdPersonCamera : MonoBehaviour
{
    private float rotLeftRight;
    private float rotUpDown;
    private float verticalRotation = 0f;
    public float mouseSensitivity = 1f;
    public float upDownRange = 90;
    private float verticalVelocity = 0f;
    public Transform head; // 머리 ><
    public float smooth = 3f;		// カメラモーションのスムーズ化用変数
	Transform standardPos;			// the usual position for the camera, specified by a transform in the game
	Transform frontPos;			// Front Camera locater
	Transform jumpPos;			// Jump Camera locater
	
	// スムーズに繋がない時（クイック切り替え）用のブーリアンフラグ
	bool bQuickSwitch = false;	//Change Camera Position Quickly
	
	
	void Start()
	{
		// 各参照の初期化
		standardPos = GameObject.Find ("CamPos").transform;
		
		if(GameObject.Find ("FrontPos"))
			frontPos = GameObject.Find ("FrontPos").transform;

		if(GameObject.Find ("JumpPos"))
			jumpPos = GameObject.Find ("JumpPos").transform;

		//カメラをスタートする
			transform.position = standardPos.position;	
			transform.forward = standardPos.forward;	
	}

	
	void FixedUpdate ()	// このカメラ切り替えはFixedUpdate()内でないと正常に動かない
	{
        FPRotate();

        if (Input.GetButton("Fire1"))	// left Ctlr
		{	
			// Change Front Camera
			setCameraPositionFrontView();
		}
		
		else if(Input.GetButton("Fire2"))	//Alt
		{	
			// Change Jump Camera
			setCameraPositionJumpView();
		}
		
		else
		{	
			// return the camera to standard position and direction
			setCameraPositionNormalView();
		}
	}

	void setCameraPositionNormalView()
	{
		if(bQuickSwitch == false){
		// the camera to standard position and direction
						transform.position = Vector3.Lerp(transform.position, standardPos.position, 1);	//카메라의 위치가 캐릭터의 얼굴 위치로 계속가는것.
						transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, 1);//카메라가 캐릭터가 보는방향을 보는것.
		}
		else{
			// the camera to standard position and direction / Quick Change
			transform.position = standardPos.position;	
			transform.forward = standardPos.forward;
			bQuickSwitch = false;
		}
	}

	
	void setCameraPositionFrontView()
	{
		// Change Front Camera
		bQuickSwitch = true;
		transform.position = frontPos.position;	
		transform.forward = frontPos.forward;
	}

	void setCameraPositionJumpView()
	{
		// Change Jump Camera
		bQuickSwitch = false;
				transform.position = Vector3.Lerp(transform.position, jumpPos.position, Time.fixedDeltaTime * smooth);	
				transform.forward = Vector3.Lerp(transform.forward, jumpPos.forward, Time.fixedDeltaTime * smooth);		
	}
    public void FPRotate()
    {
        //좌우 회전
        //rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        //transform.Rotate(0f, rotLeftRight, 0f);

        //상하 회전
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        head.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
