using Microsoft.VisualBasic;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace Icarus_Service_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<Drone> finishedList = new List<Drone>();
        Queue<Drone> expressQueue = new Queue<Drone>();
        Queue<Drone> regularQueue = new Queue<Drone>();
        
        
        
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Drone addDrone = new Drone();
            addDrone.setName(TextBoxName.Text);
            addDrone.setCost(Convert.ToDouble(TextBoxCost.Text));
            addDrone.setProblem(TextBoxProblem.Text);
            addDrone.setModel(TextBoxModel.Text);
            addDrone.setTag(100);
            expressQueue.Enqueue(addDrone);
            DisplayQueue();
         }
        public void DisplayQueue()
        {
            ListViewExpress.Items.Clear();
            foreach(Drone information in expressQueue) {

                var row = new { Client_Name=information.getName(), Drone_Model=information.getModel(), Service_Problem=information.getProblem(),
                    Service_Cost = information.getCost(),Service_Tag = information.getTag()};
                ListViewExpress.Items.Add(row); 
            }
        }
        private void setRadioButton(int serviceType)
        {
            foreach(RadioButton rb in ServiceOption.Children.OfType<RadioButton>())
            {
                if(rb.IsChecked == true)
                {

                }
            }
        }
    }
   
}
