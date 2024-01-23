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
        if (!particle.isPlaying)     // 재생 중이 아니라면 삭제
            Destroy(gameObject);
    }
}
