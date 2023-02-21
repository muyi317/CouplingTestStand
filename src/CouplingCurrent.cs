using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CouplingTestStand
{
    public class CouplingCurrent:INotifyPropertyChanged
    {
        private string couplingId = "";
        private string torqueRated = "";
        private string maxSpeed = "";
        private string torqueCons = "";
        private string axialCons = "";
        private string momentIne = "";
        private string mass = "";
        private string dataDir = "";
        private string testItem = "";
        private string testTorque = "";
        private string testFrequency = "";
        private string testStiffness = "";

        public string CouplingId
        {
            get { return couplingId; }
            set { couplingId = value; OnPropertyChanged(); }
        }
        public string TorqueRated
        {
            get { return torqueRated; }
            set { torqueRated = value; OnPropertyChanged(); }
        }
        public string MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; OnPropertyChanged(); }
        }
        public string TorqueCons
        {
            get { return torqueCons; }
            set { torqueCons = value; OnPropertyChanged(); }
        }
        public string AxialCons
        {
            get { return axialCons; }
            set { axialCons = value; OnPropertyChanged(); }
        }
        public string MomentIne
        {
            get { return momentIne; }
            set { momentIne = value; OnPropertyChanged(); }
        }
        public string Mass
        {
            get { return mass; }
            set { mass = value; OnPropertyChanged(); }
        }
        public string DataDir
        {
            get { return dataDir; }
            set { dataDir = value; OnPropertyChanged(); }
        }

        public string TestItem
        {
            get { return testItem; }
            set { testItem = value; OnPropertyChanged(); }
        }

        public string TestTorque
        {
            get { return testTorque; }
            set { testTorque = value; OnPropertyChanged(); }
        }

        public string TestFrequency
        {
            get { return testFrequency; }
            set { testFrequency = value; OnPropertyChanged(); }
        }

        public string TestStiffness
        {
            get { return testStiffness; }
            set { testStiffness = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
