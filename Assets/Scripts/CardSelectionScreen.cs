using System;
using System.Collections.Generic;
using RogueLike;
using UnityEngine;

public class CardSelectionScreen : MonoBehaviour
{
    [SerializeField] private List<CardSelectionUI> _cards;

    public void Show(List<IPowerUp> powerUps)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].Setup(i < powerUps.Count ? powerUps[i] : null, this);
        }
    }

    public void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
