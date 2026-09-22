using TMPro;
using UnityEngine;

public class BoundsControllerUI : MonoBehaviour
{
    [SerializeField] private BoundsController boundsController;
    [SerializeField] private TMP_Text boundsText;

    private void Start()
    {
        boundsController.OnBoundsChanged += UpdateText;
        UpdateText(boundsController.bounds);
    }

    private void OnDestroy()
    {
        boundsController.OnBoundsChanged -= UpdateText;
    }

    private void UpdateText(int amount)
    {
        boundsText.text = "Bounds: " + amount;
    }
}