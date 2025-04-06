using UnityEngine.UIElements;

[System.Serializable]
public class TextKeyField : VisualElement
{
    public TextField KeyField;
    public Toggle StoreKeyToggle;
        
    public TextKeyField() : base()
    {
        name = "text-key-field";
        
        KeyField = new TextField("Key: ");
        StoreKeyToggle = new Toggle("Add to Blackboard: ");
            
        contentContainer.Add(KeyField);
        contentContainer.Add(StoreKeyToggle);
    }
}
