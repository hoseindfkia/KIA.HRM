using KIA.HRM.Controllers;
using KIA.HRM_Tests.Service.Project;
using Microsoft.AspNetCore.Mvc;
using Service.Project;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Project;

namespace KIA.HRM_Tests.Controller
{
    public class ProjectControllerTest
    {
        private readonly IProjectService _service;
        private readonly ProjectController _controller; 
        public ProjectControllerTest()
        {
            _service = new ProjectServiceFake();
            _controller = new ProjectController(_service);  
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsOkFeedback()
        {
            // Act
            var okResult = _controller.GetAllAsync();

            // Assert
            Assert.IsType<Task<Feedback<IList<ProjectGetAllViewModel>>>>(okResult as Task<Feedback<IList<ProjectGetAllViewModel>>>);
        }

    }
}
