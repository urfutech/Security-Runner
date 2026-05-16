using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectableButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Настройки рамки")]
    [SerializeField] GameObject selectionBorder; // Объект-рамка
    [SerializeField] Color selectedColor = new(0.2f, 0.5f, 1f, 1f); // Голубой

    Button button;
    Image buttonImage;
    Color originalColor;
    static SelectableButton currentSelectedButton; // Статическая переменная для отслеживания текущей выбранной кнопки

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        if (buttonImage != null)
            originalColor = buttonImage.color;

        // Скрываем рамку изначально
        if (selectionBorder != null)
            selectionBorder.SetActive(false);
    }

    // Обработка клика
    public void OnPointerClick(PointerEventData eventData)
    {
        SelectButton();
    }

    // Затемнение при наведении
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null && currentSelectedButton != this)
        {
            buttonImage.color = new Color(
                originalColor.r * 0.8f,
                originalColor.g * 0.8f,
                originalColor.b * 0.8f,
                originalColor.a
            );
        }
    }

    // Возврат цвета при уходе курсора
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null && currentSelectedButton != this)
        {
            buttonImage.color = originalColor;
        }
    }

    // Выделение кнопки
    public void SelectButton()
    {
        // Снимаем выделение с предыдущей кнопки
        if (currentSelectedButton != null && currentSelectedButton != this)
        {
            currentSelectedButton.DeselectButton();
        }

        // Выделяем текущую
        currentSelectedButton = this;

        // Показываем рамку
        if (selectionBorder != null)
            selectionBorder.SetActive(true);

        // Меняем цвет на выбранный
        if (buttonImage != null)
            buttonImage.color = selectedColor;
    }

    // Снятие выделения
    public void DeselectButton()
    {
        if (selectionBorder != null)
            selectionBorder.SetActive(false);

        if (buttonImage != null)
            buttonImage.color = originalColor;
    }
}