using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface ILabOrderRepository
    {
        List<LabDataList> GetLabDataList(UserRole role);
        bool PostHl7OrderForm(LabQueryModel queryModel);
        string  CheckUserDataExist(LabQueryModel queryModel);

        List<Patients> GetPatientDetails(string userName,int userType);

        List<PatientInsurance> GetPatientInsurance(string patientID);

        string GetSSN(string patientID);
    }

   

    public class LabQueryModel
    {
        public int Status;
        public string Hl7Stream;
        public string OrderId;
        public UserRole? UserRole;
        public EntryOption? EntryOption;
        public string Email;


    }
}
