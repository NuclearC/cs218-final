using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableBehavior : MonoBehaviour
{
    public abstract void OnUse(PlayerManager playerManager);
}
