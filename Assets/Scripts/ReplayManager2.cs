using System.Collections.Generic;
using UnityEngine;

public class ReplayManager2 : MonoBehaviour
{
    private bool isInReplayMode;
    private Rigidbody rigidbody;
    private List<ActionReplayRecord> actionReplayRecords;
    private float currentReplayIndex;
    private float indexChangeRate;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        // If we are in replay mode, load from ReplayStorage
        if (ReplayStorage.StoredRecords != null && ReplayStorage.StoredRecords.Count > 0)
        {
            actionReplayRecords = new List<ActionReplayRecord>(ReplayStorage.StoredRecords);
            isInReplayMode = true;
            SetTransform(0);
            rigidbody.isKinematic = true;
        }
        else
        {
            actionReplayRecords = new List<ActionReplayRecord>();
            isInReplayMode = false;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isInReplayMode = !isInReplayMode;

            if (isInReplayMode)
            {
                SetTransform(0);
                rigidbody.isKinematic = true;
            }
            else
            {
                SetTransform(actionReplayRecords.Count - 1);
                rigidbody.isKinematic = false;
            }
        }

        indexChangeRate = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            indexChangeRate = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            indexChangeRate = -1;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            indexChangeRate *= 0.5f;
        }
    }

    private void FixedUpdate()
    {
        if (isInReplayMode == false)
        {
            actionReplayRecords.Add(new ActionReplayRecord { position = transform.position, rotation = transform.rotation });
        }
        else
        {
            float nextIndex = currentReplayIndex + indexChangeRate;

            if (nextIndex < actionReplayRecords.Count && nextIndex >= 0)
            {
                SetTransform(nextIndex);
            }
        }
    }

    private void SetTransform(float index)
    {
        currentReplayIndex = index;

        ActionReplayRecord actionReplayRecord = actionReplayRecords[(int)index];

        transform.position = actionReplayRecord.position;
        transform.rotation = actionReplayRecord.rotation;
    }

    public List<ActionReplayRecord> GetRecordedData()
    {
        return actionReplayRecords;
    }
}
