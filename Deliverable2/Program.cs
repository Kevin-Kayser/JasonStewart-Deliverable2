class MainClass

{
    public static void Main(string[] args)
    {
        // Set Variables: Guest Limit == 6, Buffet Cost == $9.99, water == Free, Softdrink == $2.00//
        double softDrinkTotal = 0;
        double waterTotal = 0;
        var reEnterGroupSize = false;
        const string padding = "    ";

        //Welcome Guests//
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Welcome to Greasy Jean's All You Can Eat Food Trough!");
        Console.WriteLine("This all you can eat experience only costs $9.99 per guest!");
        Console.WriteLine("Here at Greasy Jean's water is always free or upgrade to a 32oz softdrink for just $2.00!");
        AddPadding(padding);

        //Warn Guests About Group Size Limit//
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                     ****Please note**** \n" +
            "Due to current staffing shortages we must limit party size to 6 guests or less");
        AddPadding(padding);
        Console.ForegroundColor = ConsoleColor.White;

        //Get Number Of Guests In The Group//

        Console.WriteLine("How many guests are in your party?");
        var guestCount = int.Parse(Console.ReadLine());

        //Do Not Continue Until Valid Group Size Is Entered//
        while (!reEnterGroupSize)
        {
            AddPadding(padding);
            if (guestCount > 6)
            {
                Console.WriteLine("I'm sorry, your party of " + guestCount + " exceeds our current limit of 6 guests per party");
                AddPadding(padding);

                //Give User Chance To Adjust Group Size || Leave//
                Console.WriteLine("Would you like to adjust your party size or find a different restaurant? \n" + "Please enter Yes or y to adjust your party size or press any other key to leave");
                AddPadding(padding);

                var groupResponse = Console.ReadLine();

                //Handle User Capitalization Variance//
                groupResponse = groupResponse.ToLower();

                if (groupResponse == "yes" || groupResponse == "y")
                {
                    reEnterGroupSize = false;
                    Console.Clear();
                    Console.WriteLine("Please enter a new party size that is 6 or less guests");
                    guestCount = int.Parse(Console.ReadLine());
                }
                else
                {
                    Console.WriteLine("Sorry You Are Leaving, Please Stop By Again. \n" + "Have a nice day!");
                    return; // completes right here.
                }
            }
            else
            //Take The Group Drink Orders//
            {
                //Initiate Vars//
                var startOverCase = 0;
                reEnterGroupSize = true;

                //Loop Through Each Guest In The Group//
                for (var i = 1; i <= guestCount; i++)
                {
                    //Set ChooseBeverage == false So Each Guest Is Asked At Least Once, If ChooseBeverage == true The Guest Has Selected Their Option//
                    var chooseBeverage = false;
                    while (!chooseBeverage)
                    {
                        var guestSelection = "";
                        /*
                        Ask Guests For Drink Order
                        StartOverCase Explaination: 
                        Case 0 = Intial Ask  
                        Case 1 = Invalid Response And Try Again  
                        Case 2 = User Declined A Drink 
                        */
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (startOverCase == 0)
                            
                        {
                            Console.WriteLine("Alright Guest " + i + ", would you like water or a softdrink? \n" +
                                            "(You can type w or s for faster service)");
                            AddPadding(padding);
                        }
                        else if (startOverCase == 1)
                        {
                            Console.WriteLine("Ok lets try this again Guest " + i + ",\n" +
                                            "would you like water or a softdrink? (You can type w or s for faster service)");
                            startOverCase = 0;
                            AddPadding(padding);
                        }
                        else if (startOverCase == 2)
                        {
                            Console.WriteLine("Ok  Guest " + i + " orders nothing... Moving on.");
                            startOverCase = 0;
                            AddPadding(padding);
                            guestSelection = "none";
                        }
                        Console.ForegroundColor = ConsoleColor.White;

                        if (guestSelection != "none")
                        {
                            guestSelection = Console.ReadLine();

                            //Handle User Capitalization Variance//
                            guestSelection = guestSelection.ToLower();

                            //Allow Abbreviated User Inputs To Be Valid//
                            switch (guestSelection)
                            {
                                case "w":
                                    guestSelection = "water";
                                    break;
                                case "s":
                                    guestSelection = "softdrink";
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (guestSelection is "water" or "softdrink" or "none")
                        {
                            //Do Not Print To Console || Perform Calculations In Current Loop Iteration If GuestSelection == "none"//
                            if (guestSelection != "none") 
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine(guestSelection + ", you got it!");
                                AddPadding(padding);
                                Console.ForegroundColor = ConsoleColor.White;

                                //Set ChooseBeverage == true To Exit For Loop Iteration//
                                chooseBeverage = true;

                                //Update Running Totals Of Water && Softdrinks Ordered//
                                if (guestSelection == "water")
                                {
                                    waterTotal++;
                                }
                                else if (guestSelection == "softdrink")
                                {
                                    softDrinkTotal++;
                                }
                            }
                        }
                        else
                        //Allow User To Modify Selection If Invalid//
                        {
                            Console.WriteLine("Sorry we don't have that selection, would you like something else or nothing to drink? \n" +
                                "Enter Yes or y to choose a different drink or any key to pass");
                            AddPadding(padding);

                            var beverageResponse = Console.ReadLine();

                            //Handle User Capitalization Variance//
                            beverageResponse = beverageResponse.ToLower();

                            if (beverageResponse is "yes" or "y")
                            {
                                chooseBeverage = false;
                                startOverCase = 1;
                            }
                            else
                            {
                                //Must Set ChooseBeverage == false So "none" Will Be Stored As A Valid Selection To Exit Current Loop Iteration//
                                chooseBeverage = false;
                                startOverCase = 2;
                            }
                        }

                    }

                }
            }
        }

        // Print Receipt
        // Initiate Vars
        const double buffetCostPerPerson = 9.99;
        const double softDrinkCostPerDrink = 2.00;

        // Calculate Costs
        var totalBuffetCost = buffetCostPerPerson * guestCount;
        var totalSoftDrinkCost = softDrinkCostPerDrink * softDrinkTotal;
        var totalBill = totalBuffetCost + totalSoftDrinkCost;

        // Present Total Bill
        Console.WriteLine("Thank you for dining at Greasy Jean's!");
        AddPadding(padding);
        Console.WriteLine("Your total bill today is: $" + string.Format("{0:0.00}", totalBill));
        AddPadding(padding);

        // Write Detailed Receipt With Item Quantities && Line Item Totals
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Item            | Qty    | Cost Per   | Line Total");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine(string.Format("{0,-15} | {1,-6} | {2,-10} | {3,-10}", "Buffet", guestCount, "$" + buffetCostPerPerson, "$" + string.Format("{0:0.00}", totalBuffetCost)));

        //Do Not Show Line Items On Receipt If Zero Ordered//
        if (softDrinkTotal > 0) { Console.WriteLine(string.Format("{0,-15} | {1,-6} | {2,-10} | {3,-10}", "Softdrink", softDrinkTotal, "$" + string.Format("{0:0.00}", softDrinkCostPerDrink), "$" + string.Format("{0:0.00}", totalSoftDrinkCost))); }
        if (waterTotal > 0) { Console.WriteLine(string.Format("{0,-15} | {1,-6} | {2,-10} | {3,-10}", "Water", waterTotal, "FREE!", "$0.00")); }

        //Finish Building Receipt//
        Console.WriteLine("--------------------------------------------------");
        AddPadding(padding);
        Console.WriteLine("Total Bill ------------------------------$" + totalBill);
        AddPadding(padding);
        Console.WriteLine("------------------Add Gratuity--------------------");
        Console.WriteLine("     5%    |    10%     |    15%     |    20%     ");
        Console.WriteLine(string.Format("{0,-10} | {1,-10} | {2,-10} | {3,-10}",
            "  $" + string.Format("{0:0.00}", Math.Round((totalBuffetCost * 1.05), 2)),
            " $" + string.Format("{0:0.00}", Math.Round((totalBuffetCost * 1.10), 2)),
            " $" + string.Format("{0:0.00}", Math.Round((totalBuffetCost * 1.15), 2)),
            " $" + string.Format("{0:0.00}", Math.Round((totalBuffetCost * 1.20), 2))));
        Console.WriteLine("--------------------------------------------------");

        //Thank Guests//
        Console.ForegroundColor = ConsoleColor.White;
        AddPadding(padding);
        Console.WriteLine("Thank You! \n" + "Have a nice day!");
    }

    //Helper Method, Call To Add Space Between Console Lines//
    private static void AddPadding(string Padding)
    {
        Console.WriteLine(Padding);
    }
    
}

