using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float _width;
    
    private void Awake()
    {
        var backgroundCollider = GetComponent<BoxCollider2D>();
        _width = backgroundCollider.size.x;
    }

    private void Update()
    {
        if (transform.position.x <= -_width)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        var offset = Vector3.right * (_width * 2f);
        transform.position +=  offset;
    }
}
