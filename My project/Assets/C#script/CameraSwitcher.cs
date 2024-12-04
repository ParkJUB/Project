using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // ��ȯ�� ī�޶� �迭
    private int currentCameraIndex = 0; // ���� Ȱ��ȭ�� ī�޶� �ε���

    void Start()
    {
        // ��� ī�޶� ��Ȱ��ȭ�ϰ� ù ��° ī�޶� Ȱ��ȭ
        InitializeCameras();
    }

    void Update()
    {
        // C Ű�� ������ �� ī�޶� ��ȯ
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchToNextCamera();
        }
    }

    void InitializeCameras()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
    }

    void SwitchToNextCamera()
    {
        // ���� ī�޶� ��Ȱ��ȭ
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // ���� ī�޶�� �ε��� ������Ʈ (��ȯ)
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // �� ī�޶� Ȱ��ȭ
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
