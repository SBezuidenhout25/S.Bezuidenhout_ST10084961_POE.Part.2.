using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjPOE
{
    class Rent : Expense
    {
        //Method calculates the remaining money after expenses
        public override void calculate(double price)
        {
            availableMoney = income - price;

            foreach(double e in LstExpenses)
            {
                availableMoney -= e;
            }
            LstExpenses.Add(price);
            LstExpensesNames.Add("Rent:\t\t");
            sortExpenses();
        }

        //Method returns a string for output
        public override string ToString()
        {
            checkExpenses handler = delCheckExpenses;
            String strDisplay = "EXPENSES:";
            strDisplay += "\n------------------------------------------\n";
            for (int x = 0; x < LstExpenses.Count; x++)
            {
                strDisplay += (LstExpensesNames[x] + " \tR" + LstExpenses[x] + "\n"); 
            }
            strDisplay += "------------------------------------------";
            strDisplay += "\nMoney available = R" + Math.Round(availableMoney, 2);
            strDisplay += handler(availableMoney, income);
            return strDisplay;          
        }

    }
}
