using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public Rigidbody2D rb;
    public float shrinkSpeed = 2f;
    ScoreManager scoreManager;

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        rb.rotation = Random.Range(0f, 360f);
        transform.localScale = Vector3.one * 10f;
    }

    void Update()
    {
        transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
        if(transform.localScale.x <= .05f)
        {
            scoreManager.increaseScore();
            Destroy(gameObject);
        }
    }
}
