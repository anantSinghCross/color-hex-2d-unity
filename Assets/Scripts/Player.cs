using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 450f;
    float movement = 0f;
    float hueValue;
    public MenuManager menuManager;
    bool isClicked;
    public GameObject controlsPanel1;
    public GameObject controlsPanel2;
    private int screenWidth = Screen.width;

    private void Start()
    {
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        hueValue = Random.Range(0, 10) / 10f;
    }

    public void Update()
    {
        //movement = Input.GetAxisRaw("Horizontal");
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > ((float)screenWidth) / 2)
            {
                movement = 1f;
            }
            else if (touch.position.x < ((float)screenWidth) / 2)
            {
                movement = -1f;
            }
        }
        else
        {
            movement = 0f;
        }
        SetBackgroundColor();
    }
    private void FixedUpdate()
    {
        menuManager.BannerHide();
        transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Handheld.Vibrate();
        menuManager.EndGame();
    }
    void SetBackgroundColor()
    {
        hueValue += 0.0008f;
        if (hueValue >= 1)
        {
            hueValue = 0;
        }
        Camera.main.backgroundColor = Color.HSVToRGB(hueValue, 0.6f, 0.8f);
    }
}
