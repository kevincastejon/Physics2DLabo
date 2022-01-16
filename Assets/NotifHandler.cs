using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifHandler : MonoBehaviour
{
    [SerializeField] private Text _collisionText;
    [SerializeField] private Text _triggerText;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collisionText.color = Color.green;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _collisionText.color = Color.black;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        _triggerText.color = Color.green;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _triggerText.color = Color.black;
    }
}
