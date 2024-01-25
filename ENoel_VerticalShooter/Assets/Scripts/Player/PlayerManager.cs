using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [ContextMenu("damage")]
    public void TakeDamage()
    {
        PlayerHealth.SetHealth(PlayerHealth.GetHealth() - 1);
    }
}
