using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Awake()
    {
        Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (Renderer renderer in renderers)
        {
            if (renderer.tag == "Enemy")
            {
                renderer.material.color = color;
            }
        }
    }
}
