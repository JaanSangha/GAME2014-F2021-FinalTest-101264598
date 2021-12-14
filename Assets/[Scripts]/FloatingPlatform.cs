/* FloatingPlatform.cs
 * Jaan Sangha 101264598
 * December 14th, 2021
 * created class and added shrinking and growing of platforms based on collision
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatingPlatform : MonoBehaviour
{
    public AudioSource shrinkSound;
    public AudioSource growSound;
    public Transform start;
    public Transform end;
    public bool isActive;
    public float platformTimer;
    public float platformResetTimer;
    public float threshold;

    public PlayerBehaviour player;

    private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();

        platformTimer = 0.1f;
        platformTimer = 0;
        platformResetTimer = 0;
        isActive = false;
        distance = end.position - start.position;
    }

    // Update is called once per frame
    void Update()
    {
        platformTimer += Time.deltaTime;
        _Move();
        //if active shrink the platform by time multiplied by 0.3
        if (isActive)
        {
            platformResetTimer = 0;
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x - (Time.deltaTime * 0.3f), transform.localScale.y - (Time.deltaTime * 0.3f), transform.localScale.z);
            }

        }
        else
        {
            //if unactive make sure platform grows to normal size
            platformResetTimer += Time.deltaTime;
            if (platformResetTimer > 1.0f)
            {
                PlayGrow();
                if (transform.localScale.x < 1)
                {
                    transform.localScale = new Vector3(transform.localScale.x + (Time.deltaTime * 0.3f), transform.localScale.y + (Time.deltaTime * 0.3f), transform.localScale.z);
                }
            }
        }
  
    }

    //play grow sound effect
    public void PlayGrow()
    {
        growSound.Play();
    }
    //play shrink sound effect
    public void PlayShrink()
    {
        shrinkSound.Play();
    }
    private void _Move()
    {
        var distanceX = (distance.x > 0) ? start.position.x + Mathf.PingPong(platformTimer, distance.x) : start.position.x;
        var distanceY = (distance.y > 0) ? start.position.y + Mathf.PingPong(platformTimer, distance.y) : start.position.y;

        transform.position = new Vector3(distanceX, distanceY, 0.0f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActive = true;
        }
    }
    
    public void Reset()
    {
        transform.position = start.position;
        platformTimer = 0;
    }
}
