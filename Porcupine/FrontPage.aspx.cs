using Porcupine.App_Code;
using Porcupine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Porcupine
{
    public partial class FrontPage : System.Web.UI.Page
    {
        List<Project> projects = InitProj.Projects;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                         
            }

        }

        protected void SelectedIndexChange(object sender, EventArgs e)
        {
            //lbl_Debug.Visible = true;
            //lbl_Debug.Text = "The Control: " + sender.GetType().Name + " has changed <br />" +
            //    "To Value: " + ddl_MainProjects.SelectedValue + " <br />";
                
            
        }

       public IEnumerable<Project> GetProject()
        {
            return projects;
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<Part> partsGrid_GetData([Control] int? ddl_MainProjects)
        {
            
            IQueryable<Project> query = projects.AsQueryable();
            if (ddl_MainProjects.HasValue)
            {
                IQueryable<Part> part = query.FirstOrDefault(x => x.Id == ddl_MainProjects).Parts.AsQueryable();
                return part;
            }
            return null;
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void partsGrid_UpdateItem(int id)
        {
            var updateDataProject = new App_Code.UpdateData();
            var project = projects.FirstOrDefault(x => x.Id.ToString().Equals(ddl_MainProjects.SelectedValue));

            if (project != null)
            {
                
                var selectedPart = project.Parts.Find(x => x.Id == id);
                var oldDate = selectedPart.StartDate;

                TryUpdateModel(selectedPart);

                var newDate = selectedPart.StartDate;

                if (ModelState.IsValid)
                {
                    var diffBussinessDays = Helpers.dataTimeExtensions.diffBusinessDays(oldDate, newDate);
                    if (diffBussinessDays != 0d)
                        updateDataProject.UpdateProject(ref project, selectedPart, diffBussinessDays);
                    else
                        updateDataProject.UpdateProject(ref project);
                }
                
            }
        }
    }
}