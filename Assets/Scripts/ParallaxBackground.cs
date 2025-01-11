using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxFactor = 0.5f;

    private float length;
    private float startPosition;

    private void Awake()
    {
        cam = GameObject.Find("Main Camera");
    }

    void Start()
    {
        // Record the starting position and calculate the length of the sprite
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        // Calculate the parallax effect
        float distanceMoved = cam.transform.position.x * (1 - parallaxFactor);
        float distanceToMove = cam.transform.position.x * parallaxFactor;

        // Update the position with parallax
        transform.position = new Vector3(startPosition + distanceToMove, transform.position.y, transform.position.z);

        // Loop the background for seamless scrolling
        if (distanceMoved > startPosition + length / 2)
        {
            startPosition += length;
        }
        else if (distanceMoved < startPosition - length / 2)
        {
            startPosition -= length;
        }
    }
}
