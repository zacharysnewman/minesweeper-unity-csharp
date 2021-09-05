using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events.StateEvents;

public class CameraBehaviour : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 min = Vector2.zero;
    private Vector2 max;

    // Start is called before the first frame update
    void Start()
    {
        EventAggregator.Get<StateChangedEvent>().Subscribe(OnStateChanged);
    }

    // Update is called once per frame
    void Update()
    {
        var moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * this.moveSpeed;
        var nextPos = this.transform.position + moveInput;

        //if (WithinBounds(nextPos))
        //{
        this.transform.position = new Vector3(
            WithinBounds(nextPos.x, min.x, max.x) ? nextPos.x : this.transform.position.x,
            WithinBounds(nextPos.y, min.y, max.y) ? nextPos.y : this.transform.position.y,
            this.transform.position.z);
        //}
    }

    private bool WithinBounds(float pos, float min, float max) => pos >= min && pos <= max;

    //private bool WithinBounds(Vector3 nextPos)
    //{
    //    return nextPos.x >= this.min.x && nextPos.x <= this.max.x && nextPos.y >= this.min.y && nextPos.y <= this.max.y;
    //}

    private void OnStateChanged(State newState)
    {
        var (mapInfo, _) = newState;
        this.max = new Vector2(mapInfo.Width, mapInfo.Height);
    }
}
