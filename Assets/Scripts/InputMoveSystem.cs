using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class InputMoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        this.Entities.ForEach((ref PlayerComponent playerComponent, ref PositionComponent positionComponent, ref SpeedComponent speedComponent) =>
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                positionComponent.pos.z += Time.DeltaTime * speedComponent.speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                positionComponent.pos.z -= Time.DeltaTime * speedComponent.speed;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                positionComponent.pos.x -= Time.DeltaTime * speedComponent.speed;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                positionComponent.pos.x += Time.DeltaTime * speedComponent.speed;
            }

        });
    }
}
