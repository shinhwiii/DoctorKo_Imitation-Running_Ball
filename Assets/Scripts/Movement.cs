using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    // x축 이동
    private float moveXWidth = 1.5f;            // 1회 이동 시 이동 거리 (x축)
    private float moveTimeX = 0.1f;             // 1회 이동에 소요되는 시간 (x축) 
    private bool isXMove = false;               // true : 이동 중, false : 이동 가능
    // y축 이동
    private float originY = 0.55f;              // 점프 및 착지하는 y축 값
    private float gravity = -9.81f;             // 중력 값
    private float moveTimeY = 0.3f;             // 1회 이동에 소요되는 시간 (y축) 
    private bool isJump = false;                // true : 점프 중, false : 점프 가능
    // z축 이동
    [SerializeField]
    private float moveSpeed = 20.0f;            // 이동 속도 (z축)
    // 회전
    private float rotateSpeed = 300.0f;         // 회전 속도
    private float limitY = -1.0f;               // 플레이어가 사망하는 y 위치

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 현재 상태가 게임 시작이 아니면 Update()를 실행하지 않는다.
        if (!gameController.IsGameStart)
            return;

        // z축 이동
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

        // 오브젝트 회전 (x축)
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);

        // 낭떠러지로 떨어지면 플레이어 사망
        if (transform.position.y < limitY)
        {
            Debug.Log("gameOver");
        }
    }

    public void MoveToX(int x)
    {
        if (isXMove)     // 현재 x축 이동 중일 때 이동 불가능
            return;

        if (x > 0 && transform.position.x < moveXWidth)         // 오른쪽 이동
        {
            StartCoroutine(OnMoveToX(x));
        }
        else if (x < 0 && transform.position.x > -moveXWidth)    // 왼쪽 이동
        {
            StartCoroutine(OnMoveToX(x));
        }
    }

    public void MoveToY()
    {
        if (isJump)      // 현재 점프 중일 때 점프 불가능
            return;

        StartCoroutine(OnMoveToY());
    }

    private IEnumerator OnMoveToX(int direction)
    {
        float current = 0;
        float percent = 0;
        float start = transform.position.x;
        float end = transform.position.x + direction * moveXWidth;

        isXMove = true;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTimeX;

            float x = Mathf.Lerp(start, end, percent);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

            yield return null;
        }

        isXMove = false;
    }

    private IEnumerator OnMoveToY()
    {
        float current = 0;
        float percent = 0;
        float v0 = -gravity;    // y 방향의 초기 속도

        isJump = true;
        rigidbody.useGravity = false;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTimeY;

            // 시간 경과에 따라 오브젝트의 y 위치를 바꿔준다.
            // 포물선 운동 : 시작 위치 + (초기 속도 * 시간) + (중력 * 시간제곱)
            float y = originY + (v0 * percent) + (gravity * percent * percent);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            yield return null;
        }

        isJump = false;
        rigidbody.useGravity = true;
    }
}
