using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ServiceDesk1.wwwroot
{
    public class HR
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public short DepartmentID { get; set; }
        public string Manager { get; set; }
        public string PositionsStatus { get; set; }
        public string Title { get; set; }
        public int JobCandidateID { get; set; }
        public int? BusinessEntityID { get; set; }
        public string Resume { get; set; }

        public HR()
        {

        }
        public HR(int JobCandidateID, string Resume)
        {
            this.JobCandidateID = JobCandidateID;
            this.Resume = Resume;
        }

        public HR(int id, string description, short departmentId, string manager,string positionsStatus,string title)
        {
            this.ID = id;
            this.Description = description;
            this.DepartmentID = departmentId;
            this.Manager = manager;
            this.PositionsStatus = positionsStatus;
            this.Title = title;
        }
        
    }
}
