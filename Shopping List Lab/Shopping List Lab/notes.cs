/*
        int itemQuantity =
             //used to make sure values will not be repeatedly added to the receipt.
    if (itemContainer.Contains(shoppedItem) == true)
    {
        continue;
    }
    //used to count the instance of a specific item.
    itemQuantity = shoppingList.Where(item => shoppingList.Contains(shoppedItem)).Count();
        //used to assist with the loops for each specific item.
        int specificItemCount = itemQuantity;
    if (itemQuantity > 1)
    {
        Console.WriteLine($"There's {itemQuantity} {shoppedItem}.");
        itemContainer.Add(shoppedItem);
    }
    else
    {
        Console.WriteLine($"{shoppedItem} - {itemsForSale[shoppedItem]:c}");
        itemContainer.Add(shoppedItem);
    }
    }
}
*/
//TODO STOP THIS FROM OUTPUTTING THE QUANTITY (QUANTITY) TIMES. SO FRUSTATING COME BACK LATER
//used to hold each unqie value.