using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField]
    private GameObject square;
    [SerializeField]
    private float moveTime = 0.2f;
    private StageController stageController;

    public void SetUp(StageController stageController)
    {
        this.stageController = stageController;
    }

    public void SetInPinStuckToTarget()
    {
        StopCoroutine("MoveTo");

        square.SetActive(true);
    }

    public void MoveOneStep(float moveDistance)
    {
        StartCoroutine("MoveTo", moveDistance);
    }

    private IEnumerator MoveTo(float moveDistance)
    {
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.up * moveDistance;

        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // 태그를 스트링으로 같음을 체크할때 Equals 함수를 사용하면 편한거 같다.
        if (collision.tag.Equals("Pin"))
        {
            stageController.GameOver();
        }    
    }
}
