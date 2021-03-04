using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Container : MonoBehaviour
{
    // inventor is an implementation of the container
[System.Serializable]
    public class ContainerItem //items which are avaliable in any container
    {
        public System.Guid Id; //guid is used for creating objects with different Id's
        public string Name;
        public int Maximum; //how many it can hold

        public int amountTaken; //how many player has taken from container item

        public ContainerItem()
        {
            Id = System.Guid.NewGuid();
        }


        public int Remaining
        {
            get
            {
                return Maximum - amountTaken; 
            }
        }


        public int Get (int value)
        {
            if ((amountTaken) + value > Maximum)
            { //what it returns if you take too much
                int toMuch = (amountTaken * value) - Maximum;
                amountTaken = Maximum;
                return value - toMuch; 
            }

            amountTaken += value;
            return value; 
        }

        public void Set (int amount)
        {
            amountTaken -= amount;
            if (amountTaken < 0)
                amountTaken = 0;
        }
    
    }

    public List<ContainerItem> items;
    public event System.Action OnContainerReady;  //event to add gun to inventory 

    private void Awake()
    {
        items = new List<ContainerItem>();
        if (OnContainerReady != null)
            OnContainerReady();
    }

    public System.Guid Add(string name, int maximum)
    {
        items.Add(new ContainerItem
        {
            Maximum = maximum,
            Name = name
        });

        return items.Last().Id; 

    }

    public void Put(string name, int amount)
    {
        var containerItem = items.Where(x => x.Name == name).FirstOrDefault();
        if (containerItem == null)
            return;
        containerItem.Set(amount);
    }

    public int TakeFromContainer(System.Guid id, int amount)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem  == null)
            return -1;
        return containerItem.Get(amount);

    }

    public int GetAmountRemaining(System.Guid id)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem == null)
            return -1;
        return containerItem.Remaining;
    }

    private ContainerItem GetContainerItem(System.Guid id)
    {
        var containerItem = items.Where(x => x.Id == id).FirstOrDefault();
        return containerItem;
    }

}
