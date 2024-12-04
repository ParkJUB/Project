using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    public float maxSteeringAngle = 450f; // �ڵ��� ȸ���� �� �ִ� �ִ� ���� (�¿� �հ�)
    public float turnSpeed = 5f; // �ڵ��� ������� �ӵ�

    private float currentAngle = 0f; // �ڵ��� ���� ����
    private float targetAngle = 0f; // �ڵ��� �̵��� ��ǥ ����

    void Update()
    {
        // ����� �Է� (A, D Ű)
        float horizontalInput = Input.GetAxis("Horizontal"); // A(-1) �Ǵ� D(1)

        // ��ǥ ���� ���
        targetAngle = horizontalInput * maxSteeringAngle * 0.5f; // �¿�� ���ݾ� �̵�

        // ���� ������ ��ǥ ������ ����
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * turnSpeed);

        // �ڵ� ȸ�� ����
        transform.localRotation = Quaternion.Euler(0f, 0f, -currentAngle);
    }
}
