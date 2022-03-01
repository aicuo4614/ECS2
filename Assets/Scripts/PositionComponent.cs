using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct PositionComponent : IComponentData
{
    public Vector3 pos;
}
