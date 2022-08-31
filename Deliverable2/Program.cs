class MainClass

{
    public static void Main(string[] args)
    {
        // Set Variables: Guest Limit == 6, Buffet Cost == $9.99, water == Free, Softdrink == $2.00//
        int GuestCount = 0;
        double SoftDrinkTotal = 0;
        double WaterTotal = 0;
        bool ReEnterGroupSize = false;
        bool DoNotRunReceipt = false;
        string Padding = "    ";

        //Welcome Guests//
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Welcome to Greasy Jean's All You Can Eat Food Trough!");
        Console.WriteLine("This all you can eat experiance only costs $9.99 per guest!");
        Console.WriteLine("Here at Greasy Jean's water is always free or upgrade to a 32oz softdrink for just $2.00!");
        AddPadding(Padding);

        //Warn Guests About Group Size Limit//
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                     ****Please note**** \n" +
            "Due to current staffing shortages we must limit party size to 6 guests or less");
        AddPadding(Padding);
        Console.ForegroundColor = ConsoleColor.White;

        //Get Number Of Guests In The Group//

        Console.WriteLine("How many guests are in your party?");
        GuestCount = int.Parse(Console.ReadLine());

        //Do Not Continue Until Valid Group Size Is Entered//
        while (!ReEnterGroupSize)
        {
            AddPadding(Padding);
            if (GuestCount > 6)
            {
                Console.WriteLine("I'm sorry, your party of " + GuestCount + " exceeds our current limit of 6 guests per party");
                AddPadding(Padding);

                //Give User Chance To Adjust Group Size || Leave//
                Console.WriteLine("Would you like to adjust your party size or find a different resturant? \n" + "Please enter Yes or y to adjust your party size or press any other key to leave");
                AddPadding(Padding);

                string GroupResponse = Console.ReadLine();

                //Handle User Capitalization Variance//
                GroupResponse = GroupResponse.ToLower();

                if (GroupResponse == "yes" || GroupResponse == "y")
                {
                    ReEnterGroupSize = false;
                    Console.Clear();
                    Console.WriteLine("Please enter a new party size that is 6 or less guests");
                    GuestCount = int.Parse(Console.ReadLine());
                }
                else
                {
                    Console.WriteLine("Sorry You Are Leaving, Please Stop By Again. \n" + "Have a nice day!");
                    ReEnterGroupSize = true;
                    DoNotRunReceipt = true;
                }
            }
            else
            //Take The Group Drink Orders//
            {
                //Initiate Vars//
                int StartOverCase = 0;
                ReEnterGroupSize = true;

                //Loop Through Each Guest In The Group//
                for (int i = 1; i <= GuestCount; i++)
                {
                    //Set ChooseBeverage == false So Each Guest Is Asked At Least Once, If ChooseBeverage == true The Guest Has Selected Their Option//
                    bool ChooseBeverage = false;
                    while (!ChooseBeverage)
                    {
                        string GuestSelection = "";
                        /*
                        Ask Guests For Drink Order
                        StartOverCase Explaination: 
                        Case 0 = Intial Ask  
                        Case 1 = Invalid Response And Try Again  
                        Case 2 = User Declined A Drink 
                        */
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (StartOverCase == 0)
                            
                        {
                            Console.WriteLine("Alright Guest " + i + ", would you like water or a softdrink? \n" +
                                            "(You can type w or s for faster service)");
                            AddPadding(Padding);
                        }
                        else if (StartOverCase == 1)
                        {
                            Console.WriteLine("Ok lets try this again Guest " + i + ",\n" +
                                            "would you like water or a softdrink? (You can type w or s for faster service)");
                            StartOverCase = 0;
                            AddPadding(Padding);
                        }
                        else if (StartOverCase == 2)
                        {
                            Console.WriteLine("Ok  Guest " + i + " orders nothing... Moving on.");
                            StartOverCase = 0;
                            AddPadding(Padding);
                            GuestSelection = "none";
                        }
                        Console.ForegroundColor = ConsoleColor.White;

                        if (GuestSelection == "none")
                        {
                            GuestSelection = GuestSelection;
                        }
                        else
                        {
                            GuestSelection = Console.ReadLine();

                            //Handle User Capitalization Variance//
                            GuestSelection = GuestSelection.ToLower();

                            //Allow Abbreviated User Inputs To Be Valid//
                            switch (GuestSelection)
                            {
                                case "w":
                                    GuestSelection = "water";
                                    break;
                                case "s":
                                    GuestSelection = "softdrink";
                                    break;
                                default:
                                    GuestSelection = GuestSelection;
                                    break;
                            }
                        }
                        if (GuestSelection == "water" || GuestSelection == "softdrink" || GuestSelection == "none")
                        {
                            //Do Not Print To Console || Perform Calculations In Current Loop Iteration If GuestSelection == "none"//
                            if (GuestSelection != "none") 
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine(GuestSelection + ", you got it!");
                                AddPadding(Padding);
                                Console.ForegroundColor = ConsoleColor.White;

                                //Set ChooseBeverage == true To Exit For Loop Iteration//
                                ChooseBeverage = true;

                                //Update Running Totals Of Water && Softdrinks Ordered//
                                if (GuestSelection == "water")
                                {
                                    WaterTotal = WaterTotal + 1;
                                }
                                else if (GuestSelection == "softdrink")
                                {
                                    SoftDrinkTotal = SoftDrinkTotal + 1;
                                }
                            }
                        }
                        else
                        //Allow User To Modify Selection If Invalid//
                        {
                            Console.WriteLine("Sorry we don't have that selection, would you like something else or nothing to drink? \n" +
                                "Enter Yes or y to choose a different drink or any key to pass");
                            AddPadding(Padding);

                            string BeverageResponse = Console.ReadLine();

                            //Handle User Capitalization Variance//
                            BeverageResponse = BeverageResponse.ToLower();

                            if (BeverageResponse == "yes" || BeverageResponse == "y")
                            {
                                ChooseBeverage = false;
                                StartOverCase = 1;
                            }
                            else
                            {
                                //Must Set ChooseBeverage == false So "none" Will Be Stored As A Valid Selection To Exit Current Loop Iteration//
                                ChooseBeverage = false;
                                StartOverCase = 2;
                            }
                        }

                    }

                }
            }
        }
        //Do Not Generate A Receipt If Group Left Buffet//
        if (!DoNotRunReceipt)
        {
            //Initiate Vars//
            double BuffetCostPerPerson = 9.99;
            double SoftDrinkCostPerDrink = 2.00;
            double TotalBill = 0.00;
            double TotalBuffetCost = 0.00;
            double TotalSoftDrinkCost = 0.00;

            //Calcualte Costs//
            TotalBuffetCost = BuffetCostPerPerson * GuestCount;
            TotalSoftDrinkCost = SoftDrinkCostPerDrink * SoftDrinkTotal;
            TotalBill = TotalBuffetCost + TotalSoftDrinkCost;

            //Present Total Bill//
            Console.WriteLine("Thank you for dining at Greasy Jean's!");
            AddPadding(Padding);
            Console.WriteLine("Your total bill today is: $" + String.Format("{0:0.00}", TotalBill));
            AddPadding(Padding);

            //Write Detailed Receipt With Item Quantities && Line Item Totals//
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Item            | Qty    | Cost Per   | Line Total");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(string.Format("{0,-15} | {1,-6} | {2,-10} | {3,-10}", "Buffet", GuestCount, "$" + BuffetCostPerPerson, "$" + String.Format("{0:0.00}", TotalBuffetCost)));

            //Do Not Show Line Items On Receipt If Zero Ordered//
            if (SoftDrinkTotal > 0) { Console.WriteLine(string.Format("{0,-15} | {1,-6} | {2,-10} | {3,-10}", "Softdrink", SoftDrinkTotal, "$" + String.Format("{0:0.00}", SoftDrinkCostPerDrink), "$" + String.Format("{0:0.00}", TotalSoftDrinkCost))); }
            if (WaterTotal > 0) { Console.WriteLine(string.Format("{0,-15} | {1,-6} | {2,-10} | {3,-10}", "Water", WaterTotal, "FREE!", "$0.00")); }

            //Finish Building Receipt//
            Console.WriteLine("--------------------------------------------------");
            AddPadding(Padding);
            Console.WriteLine("Total Bill ------------------------------$" + TotalBill);
            AddPadding(Padding);
            Console.WriteLine("------------------Add Gratuity--------------------");
            Console.WriteLine("     5%    |    10%     |    15%     |    20%     ");
            Console.WriteLine(string.Format("{0,-10} | {1,-10} | {2,-10} | {3,-10}",
                    "  $" + String.Format("{0:0.00}", Math.Round((TotalBuffetCost * 1.05), 2)),
                    " $" + String.Format("{0:0.00}", Math.Round((TotalBuffetCost * 1.10), 2)),
                    " $" + String.Format("{0:0.00}", Math.Round((TotalBuffetCost * 1.15), 2)),
                    " $" + String.Format("{0:0.00}", Math.Round((TotalBuffetCost * 1.20), 2))));
            Console.WriteLine("--------------------------------------------------");

            //Thank Guests//
            Console.ForegroundColor = ConsoleColor.White;
            AddPadding(Padding);
            Console.WriteLine("Thank You! \n" + "Have a nice day!");
        }
    }

 //Helper Method, Call To Add Space Between Console Lines//
    private static void AddPadding(string Padding)
    {
        Console.WriteLine(Padding);
    }
    
}

