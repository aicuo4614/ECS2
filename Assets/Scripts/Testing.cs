using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

public class Testing : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    // 初始化调用
    void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(LevelComponent),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld)
        );

        NativeArray<Entity> entityArray = new NativeArray<Entity>(2, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);

        for (int i = 0; i < entityArray.Length; i++)
        {
            entityManager.SetComponentData(entityArray[i], new LevelComponent() { level = Random.Range(10, 20) });
            entityManager.SetComponentData(entityArray[i], new SpeedComponent() { speed = 3 });
            entityManager.SetSharedComponentData(entityArray[i], new RenderMesh() { mesh = this.mesh, material = this.material });

        }

        entityArray.Dispose();

    }

}
