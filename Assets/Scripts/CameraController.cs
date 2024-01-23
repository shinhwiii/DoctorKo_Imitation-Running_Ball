using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;   // ī�޶� �����ϴ� ���
    private float zDistance;

    private void Awake()
    {
        if (target != null)
        {
            zDistance = target.position.z - transform.position.z;
        }
    }

    private void LateUpdate()
    {
        // target�� �������� ������ �������� ����
        if (target == null)
            return;

        // ī�޶��� ��ġ ���� ����
        Vector3 position = transform.position;
        position.z = target.position.z - zDistance;
        transform.position = position;
    }
}
