using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public bool isBulletMove;
    public bool isActive = true;
    public float bulletSpeed; 
    public Vector2 moveDirection = Vector2.right; // Hareket y�n� (yukar�)


    // Start is called before the first frame update
    void Start()
    {
        if (!isActive)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBulletMove)
        {
            Vector2 direction = moveDirection.normalized;

            transform.Translate(direction * bulletSpeed * Time.deltaTime);
        }
    }
}
