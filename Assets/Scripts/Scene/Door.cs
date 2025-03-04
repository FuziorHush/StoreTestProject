using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _leftDoor;
    [SerializeField] private Transform _rightDoor;
    [SerializeField] private float _slideDistance = 2f;
    [SerializeField] private float _slideSpeed = 2f;

    private Vector3 leftDoorStartPos;
    private Vector3 rightDoorStartPos;

    private bool isOpen = false;

    void Start()
    {
        leftDoorStartPos = _leftDoor.position;
        rightDoorStartPos = _rightDoor.position;
    }

    void Update()
    {
        if (isOpen)
        {
            Vector3 targetLeftPos = leftDoorStartPos + new Vector3(0f, 0f, _slideDistance);
            Vector3 targetRightPos = rightDoorStartPos - new Vector3(0f, 0f, _slideDistance);
            _leftDoor.position = Vector3.MoveTowards(_leftDoor.position, targetLeftPos, _slideSpeed * Time.deltaTime);
            _rightDoor.position = Vector3.MoveTowards(_rightDoor.position, targetRightPos, _slideSpeed * Time.deltaTime);
        }
        else
        {
            _leftDoor.position = Vector3.MoveTowards(_leftDoor.position, leftDoorStartPos, _slideSpeed * Time.deltaTime);
            _rightDoor.position = Vector3.MoveTowards(_rightDoor.position, rightDoorStartPos, _slideSpeed * Time.deltaTime);
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
    }

    public void CloseDoor()
    {
        isOpen = false;
    }
}
