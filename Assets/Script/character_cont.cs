using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_cont : MonoBehaviour
{
    public float xSensitivity = 1.0f;
    public float character_speed = 10.0f;
    Rigidbody rigibody;
    Vector3 movement;
    public float Speed = 10f;


    Transform _transform;
    private bool _isJumping;
    private float _posY;        //오브젝트의 초기 높이
    private float _gravity;     //중력가속도
    private float _jumpPower;   //점프력
    private float _jumpTime;    //점프 이후 경과시간

    public Animator anim;

    public int chk_key = 0;

    // Use this for initialization
    private void Awake()
    {
        rigibody = GetComponent<Rigidbody>();
        _transform = transform;
        _isJumping = false;
        _posY = transform.position.y;
        _gravity = 15f;
        _jumpPower = 8.0f;
        _jumpTime = 0.0f;



    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("RunChk") == true)
        {
            Speed = 20f;
        }
        else if (anim.GetBool("RunChk") == false)
        {
            Speed = 10f;
        }
        float yRot = Input.GetAxis("Mouse X") * xSensitivity;
        this.transform.localRotation *= Quaternion.Euler(0, yRot, 0);
        Run();
        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            _isJumping = true;
            _posY = _transform.position.y;
        }

        if (_isJumping)
        {
            Jump();
        }




    }
    public void Run()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            if (chk_key > 0)
            {
                chk_key--;
            }
            
            if (chk_key == 0)
            {
                anim.SetBool("WalkChk", false);
            }

        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            chk_key++;
            anim.SetBool("WalkChk", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
            //anim.SetBool("WalkChk", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            anim.SetBool("RunChk", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            anim.SetBool("RunChk", false);
        }
    }
    void Jump()
    {
        //y=-a*x+b에서 (a: 중력가속도, b: 초기 점프속도)
        //적분하여 y = (-a/2)*x*x + (b*x) 공식을 얻는다.(x: 점프시간, y: 오브젝트의 높이)
        //변화된 높이 height를 기존 높이 _posY에 더한다.
        float height = (_jumpTime * _jumpTime * (-_gravity) / 2) + (_jumpTime * _jumpPower);
        _transform.position = new Vector3(_transform.position.x, _posY + height, _transform.position.z);
        //점프시간을 증가시킨다.
        _jumpTime += Time.deltaTime;

        //처음의 높이 보다 더 내려 갔을때 => 점프전 상태로 복귀한다.
        if (height < 0.0f)
        {
            _isJumping = false;
            _jumpTime = 0.0f;
            _transform.position = new Vector3(_transform.position.x, _posY, _transform.position.z);
        }
    }


}
