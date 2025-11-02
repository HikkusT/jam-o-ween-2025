using RogueLike;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionUI : MonoBehaviour
{
    [SerializeField] private Image _thumb;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;

    private IPowerUp _powerUp;
    private CardSelectionScreen _screen;

    public void Setup(IPowerUp powerUp, CardSelectionScreen screen)
    {
        _powerUp = powerUp;
        _screen = screen;
        gameObject.SetActive(powerUp != null);
        if (powerUp == null) return;
        
        _thumb.sprite = powerUp.Image;
        _titleText.text = powerUp.Name;
        _descriptionText.text = powerUp.Description;
    }

    public void Select()
    {
        _powerUp.Apply(FindFirstObjectByType<CharacterController>().gameObject);
        _screen.Close();
    }
}
