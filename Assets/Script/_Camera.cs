using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Camera : MonoBehaviour
{
    public Camera camera;
    public float ySensitivity = 1.0f;
    public Texture2D aim_texture;
    public Rect aim_rect;
    private RaycastHit rayHit;
    private Ray ray;
    public float MAX_RAY_DISTANCE = 500.0f;
    public bool door = false;
    public bool item = false;
    // Use this for initialization
    private void Awake()
    {
        Screen.SetResolution(2517,1527,true);
    }
    void Start()
    {
        Cursor.visible = false;
        float left = (Screen.width - aim_texture.width) / 2;
        float top = (Screen.height - aim_texture.height) / 2;
        float width = aim_texture.width;
        float height = aim_texture.height;
        aim_rect = new Rect(left, top, width, height);
    }

    // Update is called once per frame
    void Update()
    {
        float xRot = Input.GetAxis("Mouse Y") * ySensitivity;
        camera.transform.localRotation *= Quaternion.Euler(-xRot, 0, 0);

        ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out rayHit, MAX_RAY_DISTANCE))
        {
            Debug.DrawLine(ray.origin, rayHit.point, Color.green);
            Debug.Log(rayHit.transform.name);
            if (rayHit.collider.tag == "Player")
            {
                door = true;
            }
            else
            {
                door = false;
            }
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.direction * 100, Color.red);
            door = false;
        }

    }
    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 100, w, h);
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = h * 5 / 100;
        style.normal.textColor = Color.black;
        GUI.DrawTexture(aim_rect, aim_texture);
        if(door) GUI.Label(rect, "F를 눌러 문을 열으세요",style);
        else if (item) GUI.Label(rect, "F를 눌러 아이템을 얻으세요", style);

    }
}
