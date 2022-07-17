using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 이 클래스는 유니티 애니메이션에서 이벤트 함수를 호출 하기 위해 제작 된 클래스입니다.
/// </summary>
public class AnimationEventListener : MonoBehaviour
{
    [SerializeField] UnityEvent[] events;
    public void EventCall(int index)
    {
        if (index >= 0 && index < events.Length)
        {
            events[index].Invoke();

        }
        else
        {
            Debug.LogError("Index out of range");
        }
    }
}