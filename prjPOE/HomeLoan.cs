using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjPOE
{
    class HomeLoan : Expense
    {
        //Only required variables in child class
        double totalLoan;
        double monthlyPayment = 0;

        //Method calculates loan value, monthly payments and
        //available money after deductions 
        public override void calculate(double price)
        {
            availableMoney = income;

            foreach(double e in LstExpenses)
            {
                availableMoney -= e;
            }

            totalLoan = (price - deposit) * (1 + interest * (months / 12));
            monthlyPayment = (totalLoan / months);
            availableMoney -= monthlyPayment;

            LstExpenses.Add(Math.Round(monthlyPayment, 2));
            LstExpensesNames.Add("Monthly Home Payments:");
            sortExpenses();
        }


        //ToString method to return an output string
        public override string ToString()
        {
            checkExpenses handler = delCheckExpenses;
            String strOutput = "EXPENSES:";
            strOutput +="\n------------------------------------------\n";
            for (int x = 0; x < LstExpenses.Count; x++)
            {
                strOutput += (LstExpensesNames[x] + " \tR" + LstExpenses[x] + "\n");
            }
            strOutput += "------------------------------------------";
            strOutput += "\nMoney available = R" + Math.Round(availableMoney, 2);
            strOutput += "\n------------------------------------------\n";
            strOutput += handler(availableMoney, income);

            if (monthlyPayment > (income * 0.33))
            {
                strOutput += "\nYour home loan approval is unlikely";
            }

            return strOutput;
        }
    }
}
