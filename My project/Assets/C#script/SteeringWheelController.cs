using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    public float maxSteeringAngle = 450f; // 핸들이 회전할 수 있는 최대 각도 (좌우 합계)
    public float turnSpeed = 5f; // 핸들이 따라오는 속도

    private float currentAngle = 0f; // 핸들의 현재 각도
    private float targetAngle = 0f; // 핸들이 이동할 목표 각도

    void Update()
    {
        // 사용자 입력 (A, D 키)
        float horizontalInput = Input.GetAxis("Horizontal"); // A(-1) 또는 D(1)

        // 목표 각도 계산
        targetAngle = horizontalInput * maxSteeringAngle * 0.5f; // 좌우로 절반씩 이동

        // 현재 각도를 목표 각도로 보간
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * turnSpeed);

        // 핸들 회전 적용
        transform.localRotation = Quaternion.Euler(0f, 0f, -currentAngle);
    }
}
