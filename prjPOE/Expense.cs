using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjPOE
{
    public abstract class Expense
    {
        //Lists to store expenses and their names
        List<double> lstExpenses = new List<double>();
        List<string> lstExpensesNames = new List<string>()
        {
            "Groceries:\t", "Water and lights:\t","Travel costs:\t", "Cellphone and telephone:",
            "Other expenses:\t"
        };

        //Variables required to do calculations
        public double availableMoney;
        public double income;
        public double deposit;
        public double interest;
        public double months;

        //Getter and setter methods for lists
        public List<double> LstExpenses { get => lstExpenses; set => lstExpenses = value; }
        public List<string> LstExpensesNames { get => lstExpensesNames; set => lstExpensesNames = value; }

        //Delegate to determain if expenses exceed 75% of income
        public delegate string checkExpenses(double moneyAvailable, double income);
        public static string delCheckExpenses(double moneyAvailable, double income)
        {
            string strOutput = "";
            if (moneyAvailable < (income * 0.25))
            {
                strOutput += "\nExpenses exceed 75% of income";
            }
            return strOutput;
        }

        //Method to sort expenses
        public void sortExpenses()
        {
            double tempValue;
            string tempName;

            for (int x = 0; x < LstExpenses.Count - 1; x++)
            {
                for (int y = 0; y < LstExpenses.Count - 1; y++)
                {
                    if (LstExpenses[y] < LstExpenses[y + 1])
                    {
                        tempValue = LstExpenses[y + 1];
                        tempName = LstExpensesNames[y + 1];
                        LstExpenses[y + 1] = LstExpenses[y];
                        LstExpensesNames[y + 1] = LstExpensesNames[y];
                        LstExpenses[y] = tempValue;
                        LstExpensesNames[y] = tempName;
                    }
                }
            }
        }

        //Methods that are to be overrided in child classes
        public abstract void calculate(double price);
        public abstract string ToString();
    }
}
