using System.Collections;
using TMPro;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    private float fadeTime;                 // ���̵� �Ǵ� �ð�
    private TextMeshProUGUI textFade;       // ���̵� ȿ���� ����Ǵ� TMPro

    private void Awake()
    {
        textFade = GetComponent<TextMeshProUGUI>();

        // Fade ȿ���� In -> Out ���� �ݺ�
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0));        // Fade In

            yield return StartCoroutine(Fade(0, 1));        // Fade Out
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = textFade.color;
            color.a = Mathf.Lerp(start, end, percent);
            textFade.color = color;

            yield return null;
        }
    }
}
