using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOptions : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3[] spawnLocations;

    public GameObject buttonRef;

    public PlayerInput playerInput;

    public OrderChecker orderChecker;

    public float yOffset = .5f;
    private float totalOffset = 0;

    private Dictionary<string, List<string>> storage = new Dictionary<string, List<string>>();

    private Dictionary<string, string> attrColor = new Dictionary<string, string>();

    void Start()
    {
        
    }

    public void spawn(List<FoodOption> toppingArray) {
        // get topping name, acossiated gameobject
        // sprinkles, sprikles object to move into place
        // colors of sprinkles
        if (spawnLocations.Length < toppingArray.Count) {
            Debug.LogWarning("TOO MANY toppings not enough spawn locations");
        }

        for (int i = 0; i < toppingArray.Count; i++)
        {
            Transform temp = toppingArray[i].showcase.transform;
            GameObject show = Instantiate(toppingArray[i].showcase, spawnLocations[i], temp.rotation, transform);
            totalOffset += yOffset + .1f;
            HighlightParent parent = show.GetComponent<HighlightParent>();
            parent.setAttr(toppingArray[i].topping);
            
            for (int j = 0; j < toppingArray[i].possibleAttrs.Count; j++)
            {
                var item = toppingArray[i].possibleAttrs[j];
                GameObject button = Instantiate(buttonRef, spawnLocations[i] - new Vector3(0, totalOffset, 0), temp.rotation, show.transform);
                button.GetComponent<Button>().changeColor(RandomController.strColorToColor[item]);
                button.GetComponentInChildren<Highlightable>().setIndex(j);
                totalOffset += yOffset;
            }
            storage.Add(toppingArray[i].topping, toppingArray[i].possibleAttrs);
            totalOffset = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clicked(string attr, int index, bool didClick) {
        if (!didClick) {
            attrColor.Remove(attr);
            return;
        }
        attrColor[attr] = storage[attr][index];
        createDonut();
    }

    public void createDonut() {
        if(storage.Count == attrColor.Count && orderChecker.hasNextOrder) {
            List<DonutInfo> donutInfos = new List<DonutInfo>();
            foreach (var item in attrColor)
            {
                donutInfos.Add(new DonutInfo(item.Key, RandomController.strColorToColor[item.Value], item.Value));
            }
            playerInput.createDonut(donutInfos);
            reset();
        }
    }

    public void reset() {
        foreach (var item in GetComponentsInChildren<Highlightable>())
        {
            item.reset();
        }
        foreach (var item in GetComponentsInChildren<HighlightParent>())
        {
            item.reset();
        }
        attrColor = new Dictionary<string, string>();
    }
}
