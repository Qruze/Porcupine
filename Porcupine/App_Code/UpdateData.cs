using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Porcupine.Models;

namespace Porcupine.App_Code
{
    public class UpdateData
    {
        public void UpdateProject(ref Project project)
        {
            var firstPart = true;
            DateTime endDate = new DateTime();
            Project projectChanged = new Project();
            List<Part> updatedParts = new List<Part>();

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
                
                while (i <= p.NumOfDays )
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
                
                updatedParts.Add(p);                                
            }

            project.Parts = updatedParts;
        }
    }
}