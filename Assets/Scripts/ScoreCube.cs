using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCube : MonoBehaviour
{
   public Text plusOneText; // Посилання на текстовий елемент для "+1"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cube")) // Перевірка тегу зіткненого кубика
        {
            plusOneText.gameObject.SetActive(true); // Активація текстового елементу
            plusOneText.text = "+1"; // Відображення "+1" в текстовому елементі

            Invoke("ClearPlusOneText", 1.5f); // Затримка і очищення текстового елементу через 2 секунди
        }
    }

    private void ClearPlusOneText()
    {
        plusOneText.text = ""; // Очищення текстового елементу
        plusOneText.gameObject.SetActive(false); // Вимкнення текстового елементу
    }
}
