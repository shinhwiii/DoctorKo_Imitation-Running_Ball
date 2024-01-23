using UnityEngine;

public class ParticleAutoCleaner : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!particle.isPlaying)     // ��� ���� �ƴ϶�� ����
            Destroy(gameObject);
    }
}
