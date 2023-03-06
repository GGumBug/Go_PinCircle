using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField]
    private StageController stageController;
    [SerializeField]
    private GameObject pinPrefab;
    [SerializeField]
    private GameObject textPrintIndexPrefab;
    [SerializeField]
    private Transform textParent;

    [Header("Stuck Pin")]
    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private Vector3 targetPosition = Vector3.up * 2;
    [SerializeField]
    private float targetRadius = 0.8f;
    [SerializeField]
    private float pinLength = 1.5f;

    [Header("Throwable Pin")]
    [SerializeField]
    private float bottomAngle = 270;
    private List<Pin> throwablePins;

    private AudioSource audioSource;

    public void SetUp()
    {
        audioSource = GetComponent<AudioSource>();
        throwablePins = new List<Pin>();
    }

    private void Update() {
        if (!stageController.IsGameStart || stageController.IsGameOver) return;

        if (Input.GetMouseButtonDown(0) && throwablePins.Count > 0)
        {
            SetInPinStuckToTarget(throwablePins[0].transform, bottomAngle);
            // 방금 과녁에 배치한 첫 번째 핀 요소를 리스트에서 삭제
            throwablePins.RemoveAt(0);

            for (int i = 0; i < throwablePins.Count; ++ i)
            {
                throwablePins[i].MoveOneStep(stageController.TpinDistance);
            }

            stageController.DecreaseThrowablePin();

            audioSource.Play();
        }
    }

    public void SpawnThrowablePin(Vector3 position, int index)
    {
        GameObject clone = Instantiate(pinPrefab, position, Quaternion.identity);

        Pin pin = clone.GetComponent<Pin>();
        pin.SetUp(stageController);

        throwablePins.Add(pin);

        SpawnTextUI(clone.transform, index);
    }

    public void SpawnStuckPin(float angle, int index)
    {
        GameObject clone = Instantiate(pinPrefab);

        Pin pin = clone.GetComponent<Pin>();
        pin.SetUp(stageController);

        SetInPinStuckToTarget(clone.transform, angle);

        SpawnTextUI(clone.transform, index);
    }

    public void SetInPinStuckToTarget(Transform pin, float angle)
    {
        pin.position = Utils.GetPositionFromAngle(targetRadius+ pinLength, angle) + targetPosition;
        pin.rotation = Quaternion.Euler(0,0,angle);
        pin.SetParent(targetTransform);
        pin.GetComponent<Pin>().SetInPinStuckToTarget();
    }

    private void SpawnTextUI(Transform target, int index)
    {
        GameObject textClone = Instantiate(textPrintIndexPrefab);
        textClone.transform.SetParent(textParent);
        textClone.transform.localScale = Vector3.one;
        textClone.GetComponent<WorldToScreenPosition>().SetUp(target);
        // TMPro.TextMeshProUGUI 이런식으로 접근하면 using을 선언하지 않고 바로 접근할 수 있는거 같다.
        textClone.GetComponent<TMPro.TextMeshProUGUI>().text = index.ToString();
    }
}
