using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f; // �⺻ �ӵ�
    public float turnSpeed = 50f; // ȸ�� �ӵ�
    public float acceleration = 5f; // ���ӵ�
    public float brakeSpeed = 20f; // �극��ũ �ӵ�
    public int maxGears = 5; // �ִ� ��� ��
    public float[] gearSpeedLimits; // �� ����� �ӵ� �Ѱ� ��

    private Rigidbody rb;
    private float currentSpeed; // ���� �ӵ�
    private int currentGear = 1; // ���� ���
    private float currentRPM = 1000f; // ���� RPM

    private int lastGear = 1; // ���� ��� ���¸� ����

    void Start()
    {
        // Rigidbody ��������
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody�� �Ҵ����ּ���!");
        }

        // gearSpeedLimits �迭�� ������ �⺻ �� ����
        if (gearSpeedLimits.Length != maxGears)
        {
            gearSpeedLimits = new float[maxGears];
            for (int i = 0; i < maxGears; i++)
            {
                gearSpeedLimits[i] = (i + 1) * 20f; // �� ���� �ӵ� �Ѱ踦 ����
            }
        }
    }

    void FixedUpdate()
    {
        // �Է� ó��
        float moveInput = Input.GetAxis("Vertical"); // W, S Ű (����/����)
        float turnInput = Input.GetAxis("Horizontal"); // A, D Ű (��ȸ��/��ȸ��)

        // ���ӵ� �� �극��ũ ó��
        if (moveInput > 0)
        {
            // ���� ����
            currentSpeed += acceleration * Time.fixedDeltaTime;
        }
        else if (moveInput < 0)
        {
            // ����
            currentSpeed = Mathf.Max(0, currentSpeed - brakeSpeed * Time.fixedDeltaTime);
        }

        // RPM ����
        currentRPM = Mathf.Lerp(currentRPM, currentSpeed * 200f, Time.fixedDeltaTime);

        // ��� ����
        ShiftGearsBasedOnSpeed();

        // �ڵ����� �ӵ� ���
        Vector3 moveDirection = transform.forward * moveInput * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // ȸ�� ó��
        Quaternion turnRotation = Quaternion.Euler(0, turnInput * turnSpeed * Time.fixedDeltaTime, 0);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    // �ӵ��� ���� ��� ���� �Լ�
    void ShiftGearsBasedOnSpeed()
    {
        int newGear = currentGear;

        // ���� ����
        if (currentSpeed >= gearSpeedLimits[currentGear - 1] && currentGear < maxGears)
        {
            newGear++;
        }
        // ���� ����
        else if (currentSpeed < gearSpeedLimits[Mathf.Max(0, currentGear - 2)] && currentGear > 1)
        {
            newGear--;
        }

        // �� ����� ���� ������Ʈ
        if (newGear != currentGear)
        {
            currentGear = newGear;
            Debug.Log($"��� ����: {currentGear}");
        }
    }
}
