using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class MoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        this.Entities.ForEach((ref Translation translation, ref SpeedComponent speedComponent) =>
        {
            translation.Value.y += speedComponent.speed * Time.DeltaTime;
        });
    }
}
