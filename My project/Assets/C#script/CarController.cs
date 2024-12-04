using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f; // 기본 속도
    public float turnSpeed = 50f; // 회전 속도
    public float acceleration = 5f; // 가속도
    public float brakeSpeed = 20f; // 브레이크 속도
    public int maxGears = 5; // 최대 기어 수
    public float[] gearSpeedLimits; // 각 기어의 속도 한계 값

    private Rigidbody rb;
    private float currentSpeed; // 현재 속도
    private int currentGear = 1; // 현재 기어
    private float currentRPM = 1000f; // 현재 RPM

    private int lastGear = 1; // 이전 기어 상태를 저장

    void Start()
    {
        // Rigidbody 가져오기
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody를 할당해주세요!");
        }

        // gearSpeedLimits 배열이 없으면 기본 값 설정
        if (gearSpeedLimits.Length != maxGears)
        {
            gearSpeedLimits = new float[maxGears];
            for (int i = 0; i < maxGears; i++)
            {
                gearSpeedLimits[i] = (i + 1) * 20f; // 각 기어당 속도 한계를 설정
            }
        }
    }

    void FixedUpdate()
    {
        // 입력 처리
        float moveInput = Input.GetAxis("Vertical"); // W, S 키 (전진/후진)
        float turnInput = Input.GetAxis("Horizontal"); // A, D 키 (좌회전/우회전)

        // 가속도 및 브레이크 처리
        if (moveInput > 0)
        {
            // 전진 가속
            currentSpeed += acceleration * Time.fixedDeltaTime;
        }
        else if (moveInput < 0)
        {
            // 후진
            currentSpeed = Mathf.Max(0, currentSpeed - brakeSpeed * Time.fixedDeltaTime);
        }

        // RPM 증가
        currentRPM = Mathf.Lerp(currentRPM, currentSpeed * 200f, Time.fixedDeltaTime);

        // 기어 변속
        ShiftGearsBasedOnSpeed();

        // 자동차의 속도 계산
        Vector3 moveDirection = transform.forward * moveInput * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // 회전 처리
        Quaternion turnRotation = Quaternion.Euler(0, turnInput * turnSpeed * Time.fixedDeltaTime, 0);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    // 속도에 따라 기어 변속 함수
    void ShiftGearsBasedOnSpeed()
    {
        int newGear = currentGear;

        // 상향 변속
        if (currentSpeed >= gearSpeedLimits[currentGear - 1] && currentGear < maxGears)
        {
            newGear++;
        }
        // 하향 변속
        else if (currentSpeed < gearSpeedLimits[Mathf.Max(0, currentGear - 2)] && currentGear > 1)
        {
            newGear--;
        }

        // 기어가 변경될 때만 업데이트
        if (newGear != currentGear)
        {
            currentGear = newGear;
            Debug.Log($"기어 변경: {currentGear}");
        }
    }
}
