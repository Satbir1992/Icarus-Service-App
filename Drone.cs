using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus_Service_App
{
    internal class Drone
    {
        private string client_Name;
        private string drone_Model;
        private string service_Problem;
        private double service_Cost;
        private int service_Tag;

       public Drone()
        {

        }
        public Drone(string aclient_Name, string adrone_Model, string aservice_Problem, double aservice_Cost, int aservice_Tag)
        {
            client_Name = aclient_Name;
            drone_Model = adrone_Model;
            service_Problem = aservice_Problem;
            service_Cost = aservice_Cost;
            service_Tag = aservice_Tag;
        }
        public void setName(string aClientName)
        {
            client_Name=aClientName;
        }
        public string getName()
        {
            return client_Name;
        }
        public void setModel(string aDroneModel)
        {
            drone_Model = aDroneModel;
        }
        public string getModel()
        {
            return drone_Model;
        }
        public void setProblem(string aServiceProblem)
        {
            service_Problem = aServiceProblem;
        }
        public string getProblem()
        {
            return service_Problem;
        }
        public void setCost(double aServiceCost)
        {
            service_Cost=aServiceCost;
        }
        public double getCost()
        {
            return (service_Cost);
        }
        public void setTag(int aServiceTag)
        {
            service_Tag=aServiceTag;
        }
        public int getTag()
        {
            return service_Tag;
        }
}
}
