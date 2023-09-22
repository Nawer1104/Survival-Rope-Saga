using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool _dragging = true;

    private Vector2 _offset;

    public static bool mouseButtonReleased;

    private Vector3 startPos;

    public GameObject vfxDestroy;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnMouseDown()
    {
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        if (!_dragging) return;

        var mousePosition = GetMousePos();

        transform.position = mousePosition - _offset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Obstacle"))
        {
            GameObject vfx = Instantiate(vfxDestroy, transform.position, Quaternion.identity) as GameObject;
            Destroy(vfx, 1f);

            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            GameManager.Instance.CheckLevelUp();
        }
    }

    private void OnMouseUp()
    {
        mouseButtonReleased = true;

        if (_dragging)
        {
            transform.position = startPos;
        }
    }

    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


}