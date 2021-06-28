using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private Transform playerBody;
    public PlayerMovement player;
    

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (IsRightMouseButtonUp()) // this is important for menu scence !!!!!!!!!!! ####################
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (player.moveIsEnable)
        {
            rotateCam();
        }

    }

    private void rotateCam()
    {
        float horizontal = Input.GetAxis(horizontalInputName) * Time.deltaTime * 110;
        playerBody.rotation = playerBody.rotation * Quaternion.Euler(0, horizontal, 0);
    }

    bool IsRightMouseButtonUp()
    {
        return Input.GetMouseButtonUp(1);
    }
    bool IsEscapePressed()
    {
        return Input.GetKey(KeyCode.Escape);
    }
}
