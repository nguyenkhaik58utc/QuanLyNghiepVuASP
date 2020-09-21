using EntityFW.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigExample.Areas.Admin.Model
{
    public class EmployeeModel
    {

        public int Employee_ID { get; set; }

        public string User_emp { get; set; }

        public string Employee_Name { get; set; }

        public string Department { get; set; }

        public DateTime? Date_Of_Birth { get; set; }

        public string Sex { get; set; }

        public string Images { get; set; }

        public string Address_emp { get; set; }

        public string Email_Address { get; set; }

        public string Phone_Number { get; set; }

        public string Create_by { get; set; }

        public DateTime? Create_at { get; set; }

        public string Update_by { get; set; }

        public DateTime? Update_at { get; set; }

        public string Delete_by { get; set; }

        public DateTime? Delete_at { get; set; }

        public int Delete_flag { get; set; }

        public EmployeeModel(int employee_ID, string user_emp, string employee_Name, string department, DateTime? date_Of_Birth, string sex, string images, string address_emp, string email_Address, string phone_Number)
        {
            Employee_ID = employee_ID;
            User_emp = user_emp;
            Employee_Name = employee_Name;
            Department = department;
            Date_Of_Birth = date_Of_Birth;
            Sex = sex;
            Images = images;
            Address_emp = address_emp;
            Email_Address = email_Address;
            Phone_Number = phone_Number;
        }

        public EmployeeModel(int employee_ID, string employee_Name, string department, DateTime? date_Of_Birth, string sex, string images, string address_emp, string email_Address, string phone_Number)
        {
            Employee_ID = employee_ID;
            Employee_Name = employee_Name;
            Department = department;
            Date_Of_Birth = date_Of_Birth;
            Sex = sex;
            Images = images;
            Address_emp = address_emp;
            Email_Address = email_Address;
            Phone_Number = phone_Number;
        }

        public EmployeeModel()
        {
        }

        public EmployeeModel(int employee_ID, string employee_Name, string images, string user_emp, string department, DateTime? date_Of_Birth, string sex, string address_emp, string email_Address, string phone_Number)
        {
            Employee_ID = employee_ID;
            Employee_Name = employee_Name;
            Images = images;
            User_emp = user_emp;
            Department = department;
            Date_Of_Birth = date_Of_Birth;
            Sex = sex;
            Address_emp = address_emp;
            Email_Address = email_Address;
            Phone_Number = phone_Number;
        }

        public static implicit operator EmployeeModel(Employee v)
        {
            throw new NotImplementedException();
        }
    }
}