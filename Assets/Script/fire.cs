using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour {
    //public Transform transform;
    public GameObject bullet;
    public GameObject Gun_bolt_carrier;
    //public Vector3 carrier_pos; // 노리쇠 뭉치의 local위치를 저장시킬 변수.
	// Use this for initialization
	void Start () {
        //carrier_pos = Gun_bolt_carrier.GetComponent<Transform>().localPosition; // 노리쇠 뭉치의 local위치를 불러와 저장시킴.
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) _Fire(); // 마우스 왼쪽버튼 입력을 받으면 _Fire()함수 실행.
	}
    void _Fire() 
    // 총알발사
    {
        Gun_bolt_carrier.gameObject.GetComponent<Animator>().Play("Base Layer.bolt_carrier");
        //Gun_bolt_carrier.transform.localPosition = carrier_pos; 
        // 노리쇠 뭉치오브젝트의 위치를 재정의함.
        //Gun_bolt_carrier.transform.Translate(Vector3.up*5/12); 
        // 노리쇠 뭉치오브젝트의 위치를 임의의 값만큼 이전 프레임과 현재 프레임 사이의 시간을 이용하여 부드럽게 이동시킴.
        createBullet();
        // 총알 오브젝트 생성.
    }
    void createBullet()
    {
        Instantiate(bullet, transform.position, transform.rotation); // 총알 오브젝트를 총오브젝트의 위치와 방향을 고려하여 생성 후 발사.
    }
}
