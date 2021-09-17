using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexStore.model
{
    public class Customer
    {
        private int id;
        private string email;
        private string password;
        private string first_name;
        private string last_name;
        private string country;
        private string phone;
        private string adress;

        //setters && getters
        #region
        public int ID { get => this.id; set => this.id = value; }
        public string Email { get => this.email; set => this.email = value; }
        public string Password { get => this.password; set => this.password = value; }
        public string First_Name { get => this.first_name; set => this.first_name = value; }
        public string Last_Name { get => this.last_name; set => this.last_name = value; }
        public string Country { get => this.country; set => this.country = value; }
        public string Phone { get => this.phone; set => this.phone = value; }
        public string Adress { get => this.adress; set => this.adress = value; }
        #endregion

        public Customer(int id, string email, string password, string first_name, string last_name, string country, string phone, string adress)
        {
            this.id = id;
            this.email = email;
            this.password = password;
            this.first_name = first_name;
            this.last_name = last_name;
            this.country = country;
            this.phone = phone;
            this.adress = adress;
        }

        public override string ToString()
        {
            string adressToString = string.Empty;
            string[] words = adress.Split(',');
            adressToString = "\n\tCounty/State: " + words[0];
            adressToString += "\n\tCity/Town/Village: " + words[1];
            adressToString += "\n\tStreet: " + words[2];
            adressToString += "\n\tBlock name/House number: " + words[3];
            return "Customer ID: " + id.ToString() + "\nEmail: " + email + "\nPassword: " + password + "\nFirst name: " + first_name
            + "\nLast name: " + last_name + "\nCountry: " + country + "\nPhone number: " + phone + "\nAdress: " + adressToString;
        }
        public override bool Equals(object obj)
        {
            Customer other = (Customer)obj;
            return id == other.id;
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}
