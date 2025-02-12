using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class waitButtonPress : MonoBehaviour
{
    private Button targetButton;
 List<(Object, MethodInfo)> actions = new List<(Object, MethodInfo)>();

    void Awake(){
        targetButton = GetComponent<Button>();
        if (targetButton != null)
        {
            HandleButtonClick();
        }

    }
    

     void HandleButtonClick()
    {
        
        UnityEvent buttonEvent = targetButton.onClick;
        int count = buttonEvent.GetPersistentEventCount();
        

        // Collect all persistent methods
        for (int i = 0; i < count; i++)
        {
            Object target = buttonEvent.GetPersistentTarget(i);
            string methodName = buttonEvent.GetPersistentMethodName(i);

            if (target != null && !string.IsNullOrEmpty(methodName))
            {
                MethodInfo method = target.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (method != null)
                {
                    actions.Add((target, method));
                }
            }
        }

        // Remove all but the first listener
        if (actions.Count > 1)
        {
            targetButton.onClick.RemoveAllListeners();
            //targetButton.onClick.AddListener(() => actions[0].Item2.Invoke(actions[0].Item1, null));
        }
        Debug.Log(actions[1]);

        // Invoke each function with delay
       
    }
  
    IEnumerator WaitForButtonAnimation(){
        yield return new WaitForSeconds(1f);
        foreach (var (target, method) in actions)
        {
            
            method.Invoke(target, null);
            
            
        }


    }
    public void ButtonClick(){
        StartCoroutine(WaitForButtonAnimation());

    }
}