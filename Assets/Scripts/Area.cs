using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField]
    private float destroyDistance = 15;
    private AreaSpawner areaSpawner;
    private Transform playerTransform;

    public void SetUp(AreaSpawner areaSpawner, Transform playerTransform)
    {
        this.areaSpawner = areaSpawner;
        this.playerTransform = playerTransform;
    }

    private void Update()
    {
        if (playerTransform.position.z - transform.position.z >= destroyDistance)
        {
            // 货肺款 备开阑 积己
            areaSpawner.SpawnArea();
            // 泅犁 备开篮 昏力
            Destroy(gameObject);
        }
    }
}
