using UnityEngine;
using UnityEngine.UI;

public class ChildButtonHandler : MonoBehaviour
{
    [SerializeField] Button[] childButtons;
    private void Start()
    {
        // Get all the child buttons
        
    }
    public void readchild()
    {
        childButtons = GetComponentsInChildren<Button>();
        int numChildButtons = childButtons.Length;

        // Add click listeners to each child button
        for (int i = 0; i < numChildButtons; i++)
        {
            int childNumber = i + 1; // Child number is 1-indexed
            Debug.Log("changes were made in the process");
            //childButtons[i].onClick.AddListener(() => childButtons[i].GetComponent<ReceivablePrefab>().showdata(i));//OnChildButtonClicked(childNumber));
            
        }
    }

    private void OnChildButtonClicked(int childNumber)  
    {
        //ReceiveablePrefab.instance.
        Debug.Log("Child Button " + childNumber + " pressed.");
        //prefab
        // You can perform any specific actions or logic based on the childNumber here.
    }
}
