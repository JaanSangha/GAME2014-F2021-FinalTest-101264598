using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatingPlatform : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public bool isActive;
    public float platformTimer;
    public float threshold;

    public PlayerBehaviour player;

    private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();

        platformTimer = 0.1f;
        platformTimer = 0;
        isActive = false;
        distance = end.position - start.position;
    }

    // Update is called once per frame
    void Update()
    {
        platformTimer += Time.deltaTime;
        _Move();
        if (isActive)
        {
            transform.localScale = new Vector3(4,4,4);
            Debug.Log("transform scale");
        }
        //if (isActive)
        //{
        //    platformTimer += Time.deltaTime;
        //   // _Move();
        //}
        //else
        //{
        //    if (Vector3.Distance(player.transform.position, start.position) <
        //        Vector3.Distance(player.transform.position, end.position))
        //    {
        //        if (!(Vector3.Distance(transform.position, start.position) < threshold))
        //        {
        //            platformTimer += Time.deltaTime;
        //           // _Move();
        //        }
        //    }
        //    else
        //    {
        //        if (!(Vector3.Distance(transform.position, end.position) < threshold))
        //        {
        //            platformTimer += Time.deltaTime;
        //            //_Move();
        //        }
        //    }
        //}
    }

    private void _Move()
    {
        var distanceX = (distance.x > 0) ? start.position.x + Mathf.PingPong(platformTimer, distance.x) : start.position.x;
        var distanceY = (distance.y > 0) ? start.position.y + Mathf.PingPong(platformTimer, distance.y) : start.position.y;

        transform.position = new Vector3(distanceX, distanceY, 0.0f);
    }

    IEnumerator AnimateTextCoroutine(float secondsPerCharacter = 0.1f)
    {
        transform.localScale = new Vector3(transform.localScale.x * (Time.deltaTime * -0.1f), transform.localScale.y, transform.localScale.z);

        ////then over time, add letters until complete
        //for (int currentChar = 0; currentChar < message.Length; currentChar++)
        //{
        //    encounterTextBox.text += message[currentChar];
            yield return new WaitForSeconds(secondsPerCharacter);
        //}
        ////Debug.Log("Animated Text");
        ////abilitiesPanel.SetActive((true));
        //animateTextCoroutine = null;
    }

    public void Shrink()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActive = true;
            Debug.Log("collision wit plat");
        }
    }

    public void Reset()
    {
        transform.position = start.position;
        platformTimer = 0;
    }
}
