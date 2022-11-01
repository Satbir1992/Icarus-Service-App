using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus_Service_App
{
    //6.1	Create a separate class file to hold the data items of the Drone. Use separate getter and setter methods, ensure the attributes are private
    //and the accessor methods are public. Add a display method that returns a string for Client Name and Service Cost. Add suitable code to the
    //Client Name and Service Problem accessor methods so the data is formatted as Title case or Sentence case. Save the class as “Drone.cs”.
    internal class Drone
    {
        private string client_Name;
        private string drone_Model;
        private string service_Problem;
        private double service_Cost;
        private int service_Tag;

       public Drone()
        {
            client_Name = "";
            drone_Model = "";
            service_Problem = "";
            service_Cost = 0;
            service_Tag = 100;
        }
        public void SetName(string aClientName)
        {
            client_Name=aClientName;
        }
        public string GetName()
        {
            return client_Name;
        }
        public void SetModel(string aDroneModel)
        {
            drone_Model = aDroneModel;
        }
        public string GetModel()
        {
            return drone_Model;
        }
        public void SetProblem(string aServiceProblem)
        {
            service_Problem = aServiceProblem;
        }
        public string GetProblem()
        {
            return service_Problem;
        }
        public void SetCost(double aServiceCost)
        {
            service_Cost=aServiceCost;
        }
        public double GetCost()
        {
            return service_Cost = Math.Round(service_Cost,1);
        }
        public void SetTag(int aServiceTag)
        {
            service_Tag=aServiceTag;
        }
        public int GetTag()
        {
            return service_Tag;
        }
}
}
