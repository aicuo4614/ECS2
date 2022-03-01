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
    public GameObject prefab;
    // 初始化调用
    void Start()
    {
        var setting = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefabEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, setting);
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        //EntityArchetype entityArchetype = entityManager.CreateArchetype(
        //    typeof(LevelComponent),
        //    typeof(Translation),
        //    //typeof(RenderMesh),
        //    typeof(LocalToWorld),
        //    typeof(SpeedComponent)
        //);

        //NativeArray<Entity> entityArray = new NativeArray<Entity>(2, Allocator.Temp);

        for (int i = 0; i < 1; i++)
        {
            var instance = entityManager.Instantiate(prefabEntity);
            //entityManager.SetArchetype(instance, entityArchetype);
            entityManager.AddComponentData(instance, new SpeedComponent() { speed = 0.5f });
            entityManager.AddComponentData(instance, new PositionComponent() { pos = new Vector3(0, 0, 0) });

            //标记为主角
            entityManager.AddComponentData(instance, new PlayerComponent() { });

        }

        //entityManager.Instantiate(prefabEntity, entityArray);
        //entityManager.CreateEntity(entityArchetype, entityArray);


        //for (int i = 0; i < entityArray.Length; i++)
        //{
        //    entityManager.SetComponentData(entityArray[i], new LevelComponent() { level = Random.Range(10, 20) });
        //    entityManager.SetComponentData(entityArray[i], new SpeedComponent() { speed = 3 });
        //    //entityManager.SetSharedComponentData(entityArray[i], new RenderMesh() { mesh = this.mesh, material = this.material });

        //}

        //entityArray.Dispose();

    }

}
