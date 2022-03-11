using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] guns;
    [SerializeField] private float difficultyIncreaseRate;
    [SerializeField] private PlatformMover[] platforms;

    private List<GameObject> inactiveGuns = new List<GameObject>();
    private List<PlatformMover> stationaryPlatforms = new List<PlatformMover>();
    private int gunToActivate;
    private int platformToMove;
    private float currentChance;

    private void Awake()
    {
        inactiveGuns.AddRange(guns);
        stationaryPlatforms.AddRange(platforms);
    }

    private void Start()
    {
        currentChance = 0.5f;
        InvokeRepeating("IncreaseDifficultyViaTime", difficultyIncreaseRate, 
            difficultyIncreaseRate);
    }

    private void IncreaseDifficultyViaTime()
    {
        if (stationaryPlatforms.Count == 0) return;

        platformToMove = Random.Range(0, stationaryPlatforms.Count);

        if (stationaryPlatforms[platformToMove].movingX)
        {
            stationaryPlatforms[platformToMove].StartYMotion();
            stationaryPlatforms.RemoveAt(platformToMove);
            return;
        }

        if (stationaryPlatforms[platformToMove].movingY)
        {
            stationaryPlatforms[platformToMove].StartXMotion();
            stationaryPlatforms.RemoveAt(platformToMove);
            return;
        }

        if (Random.value >= currentChance)
        {
            stationaryPlatforms[platformToMove].StartXMotion();
            currentChance += 0.1f;
        }
        else
        {
            stationaryPlatforms[platformToMove].StartYMotion();
            currentChance -= 0.1f;
        }
    }

    public void IncreaseDifficultyViaProgression()
    {
        if (inactiveGuns.Count == 0) return;

        gunToActivate = Random.Range(0, inactiveGuns.Count);

        inactiveGuns[gunToActivate].SetActive(true);

        inactiveGuns.RemoveAt(gunToActivate);
    }
}
