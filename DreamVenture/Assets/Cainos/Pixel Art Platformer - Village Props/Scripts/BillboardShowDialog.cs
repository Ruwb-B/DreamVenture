using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardShowDialog : MonoBehaviour
{
    [SerializeField] GameObject _dialog;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHip")
        {
            _dialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHip")
        {
            _dialog.SetActive(false);
        }
    }
}
