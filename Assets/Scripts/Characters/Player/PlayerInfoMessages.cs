using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace BGS.Task
{
    public class PlayerInfoMessages : MonoBehaviour
    {
        [SerializeField] private GameObject textObject;
        [SerializeField] private RectTransform textParent;
        [SerializeField] private float moveY = 0.1f;
        [SerializeField] private float moveDuration = 4f;
        [SerializeField] private float lifetime = 6f;
        [SerializeField] private Ease moveEase;
        [SerializeField] private int spacing = 20;
        private List<GameObject> textObjects = new();
        private const int maxTextsAlive = 6;
        
        
        public void ShowText(string message, Color color)
        {
            if (textObjects.Count >= maxTextsAlive)
            {
                RemoveOldestText();
            }

            var text = Instantiate(textObject, textParent);
            var tmpro = text.GetComponentInChildren<TextMeshProUGUI>();
            tmpro.color = color;
            tmpro.text = message;

            // Calculate the initial Y position based on the number of existing text objects and spacing
            float initialYPosition = -(spacing * textObjects.Count);
            text.transform.localPosition = new Vector3(0, initialYPosition, 0);

            // Fade out and destroy the text after its lifetime
            tmpro.DOFade(0, moveDuration - .5f).SetDelay(lifetime - moveDuration).OnComplete(() => {
                textObjects.Remove(text);
                Destroy(text.gameObject);
            });

            // Move up existing texts smoothly
            for (int i = 0; i < textObjects.Count; i++)
            {
                GameObject existingText = textObjects[i];
                // Calculate the new Y position for each existing text
                float newYPosition = -(spacing * i) + spacing; // Move each text up by one spacing unit
                existingText.transform.DOLocalMoveY(newYPosition, moveDuration).SetEase(moveEase);
            }

            textObjects.Add(text);

            // This check is now redundant since we handle it at the start, but kept for clarity
            if (textObjects.Count > maxTextsAlive)
            {
                RemoveOldestText();
            }
        }

        private void RemoveOldestText()
        {
            var toRemove = textObjects[0];
            textObjects.RemoveAt(0);
            toRemove.GetComponentInChildren<TextMeshProUGUI>().DOFade(0, 0.5f).OnComplete(() => {
                Destroy(toRemove.gameObject);
            });
        }

        public void ClearTexts()
        {
            foreach (var text in textObjects)
            {
                Destroy(text);
            }
            textObjects.Clear();
        }
        
        
    }
}
