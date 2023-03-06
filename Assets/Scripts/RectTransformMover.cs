using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RectTransformMover : MonoBehaviour
{
    //이벤트 등록 및 이벤트에 등록된 메소드 호출을 위한 이벤트 클래스, 클래스안에는 내용이 따로 없음
    private class EndMoveEvent : UnityEvent { }
    private EndMoveEvent onEndMoveEvent;

    [SerializeField]
    private float moveTime = 1.0f;
    private RectTransform rectTransform;
    private bool isMoved = false;

    private void Awake() {
        onEndMoveEvent = new EndMoveEvent();
        rectTransform = GetComponent<RectTransform>();
    }

    public void MoveTo(UnityAction action, Vector3 position)
    {
        if (!isMoved)
        {
            onEndMoveEvent.AddListener(action);

            StartCoroutine(OnMove(action, position));
        }
    }

    private IEnumerator OnMove(UnityAction action, Vector3 end)
    {
        float current = 0;
        float percent = 0;
        Vector3 start = rectTransform.anchoredPosition;

        isMoved = true;

        while (percent < 1)
        {
            // Time.deltaTime 한 프레임이 끝나기까지 걸린 시간.
            current += Time.deltaTime;
            percent = current / moveTime;

            rectTransform.anchoredPosition = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        isMoved = false;

        onEndMoveEvent.Invoke();

        onEndMoveEvent.RemoveListener(action);
    }
}
