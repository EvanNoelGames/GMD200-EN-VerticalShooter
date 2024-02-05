using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerWeaponsManager
{
    // determines how fast the player can shoot their laser
    public static float timeBetweenShots = 0.75f;
    // determines if the player's lasers get destroyed after hitting an enemy
    public static bool penetrationRounds = false;
    public static bool shotgun = false;
    // determines how fast the player's lasers travel
    public static float laserSpeed = 10f;

    public static void ResetEverything()
    {
        timeBetweenShots = 0.75f;
        penetrationRounds = false;
        laserSpeed = 10f;
        shotgun = false;
    }

    public static float GetTimeBetweenShots()
    {
        return timeBetweenShots;
    }

    public static void SetTimeBetweenShots(float newValue)
    {
        timeBetweenShots = newValue;
    }

    public static bool GetPenetrationRounds()
    {
        return penetrationRounds;
    }

    public static void SetPenetrationRounds(bool newValue)
    {
        penetrationRounds = newValue;
    }

    public static float GetLaserSpeed()
    {
        return laserSpeed;
    }

    public static void SetLaserSpeed(float newValue)
    {
        laserSpeed = newValue;
    }
}
