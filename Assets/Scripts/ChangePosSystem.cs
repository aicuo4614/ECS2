using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class ChangePositionSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        this.Entities.ForEach((ref Translation translation, ref PositionComponent positionComponent) =>
        {
            translation.Value = positionComponent.pos;

        });
    }
}
