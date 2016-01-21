using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Porcupine.Models;

namespace Porcupine.App_Code
{
    public static class InitProj
    {
        static List<Project> _projects;

        public static List<Project> Projects
        {
            get
            {
                if (_projects == null)
                {
                    _projects = CreateInitProjects();
                }
                return _projects;
            }
            set
            {
                _projects = value;
            }
        }

        public static List<Project> CreateInitProjects()
        {
            var initProjects = new List<Project>();
            var parts = new List<Part>();
            var project = new Project { Id = 1, Name = "First Project", Parts = parts };

            var partA = new Part { Id = 1, ProjectId = 1, Name = "A", StartDate = new DateTime(2016, 1, 18), NumOfDays = 6, OnlyWorkDays = true };
            parts.Add(partA);

            var partB = new Part { Id = 2, ProjectId = 1, Name = "B", StartDate = new DateTime(2016, 1, 26), NumOfDays = 5, OnlyWorkDays = false };
            parts.Add(partB);

            var partC = new Part { Id = 3, ProjectId = 1, Name = "C", StartDate = new DateTime(2016, 2, 1), NumOfDays = 1, OnlyWorkDays = true };
            parts.Add(partC);

            initProjects.Add(project);

            var parts2 = new List<Part>();
            var project2 = new Project { Id = 2, Name = "Second Project", Parts = parts2 };
            var part2A = new Part { Id = 1, ProjectId = 2, Name = "A2", StartDate = new DateTime(2014, 1, 18), NumOfDays = 6, OnlyWorkDays = true };
            parts2.Add(part2A);

            initProjects.Add(project2);

            return initProjects;
        }        
    }
}