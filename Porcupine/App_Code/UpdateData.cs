using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Porcupine.Models;

namespace Porcupine.App_Code
{
    public class UpdateData
    {
        bool firstPart = true;
        DateTime endDate = new DateTime();
        public void UpdateProject(ref Project project, Part updatedPart = null, double? diffBussnissDays = null)
        {
            
            Project projectChanged = new Project();
            List<Part> newParts = new List<Part>();

            if (updatedPart != null && updatedPart.Id > 1)
            {
                var updatedDate = updatedPart.StartDate;
                var updatedId = updatedPart.Id;
                var updatedNumOfDays = updatedPart.NumOfDays;
                var updatedOnlyWorkDays = updatedPart.OnlyWorkDays;

                var prevPart = project.Parts.First(x => x.Id == 1);
                var prevPartNumOfWorkDays = prevPart.NumOfDays;
                var prevPartOnlyWorkDays = prevPart.OnlyWorkDays;
                var newStartDate = Helpers.dataTimeExtensions.addBusinessDays(prevPart.StartDate, (int)diffBussnissDays);
                newStartDate = Helpers.dataTimeExtensions.getThisOrPrevWorkday(newStartDate);

                project.Parts.First(x => x.Id == 1).StartDate = newStartDate;
            }
            newParts = updateInfo(ref project);

            project.Parts = newParts;
        }

        private List<Part> updateInfo(ref Project project)
        {
            List<Part> newParts = new List<Part>();
            foreach (Part p in project.Parts)
            {
                DateTime startDate;
                if (firstPart)
                {
                    startDate = p.StartDate;
                    firstPart = false;
                }
                else
                {
                    startDate = endDate;
                    p.StartDate = startDate;
                }
                var dayOfWeek = startDate.DayOfWeek;

                var i = 1;

                while (i <= p.NumOfDays)
                {
                    if (p.OnlyWorkDays)
                    {
                        if (dayOfWeek == DayOfWeek.Saturday)
                        {
                            startDate = startDate.AddDays(3);
                        }
                        else if (dayOfWeek == DayOfWeek.Sunday)
                        {
                            startDate = startDate.AddDays(2);
                        }
                        else
                        {
                            startDate = startDate.AddDays(1);
                        }
                    }
                    else
                    {
                        startDate = startDate.AddDays(1);
                    }


                    endDate = startDate;
                    dayOfWeek = endDate.DayOfWeek;
                    i++;
                }

                newParts.Add(p);
            }
            return newParts;
        }
    }
}