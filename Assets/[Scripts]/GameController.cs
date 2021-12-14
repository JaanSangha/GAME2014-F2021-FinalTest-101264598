using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Bullet Related")]
    public int MaxBullets;
    public BulletType enemyBulletType;
    public BulletType playerBulletType;

    [Header("Moving Platforms")] 
    public List<MovingPlatformController> movingPlatforms;

    [Header("Floating Platforms")]
    public List<FloatingPlatform> floatingPlatforms;

    // Start is called before the first frame update
    void Start()
    {
        movingPlatforms = FindObjectsOfType<MovingPlatformController>().ToList();
        floatingPlatforms = FindObjectsOfType<FloatingPlatform>().ToList();

        // Kickoff the BulletManager
        BulletManager.Instance().Init(MaxBullets, enemyBulletType, playerBulletType);
    }

    public void ResetAllPlatforms()
    {
        foreach (var platform in movingPlatforms)
        {
            platform.Reset();
        }
    }

}
