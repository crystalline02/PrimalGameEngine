using PrimalEditor.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.GameProject
{
    [DataContract]
    public class ProjectData
    {
        [DataMember]
        public string? ProjectName { get; set; }
        [DataMember]
        public string? ProjectPath { get; set; }
        [DataMember]
        public DateTime? LastOpenTime { get; set; }

        public string FilePath => Path.Combine(ProjectPath ?? "", $"{ProjectName}{Project.Extention}");
        public byte[]? Icon { get; set; }
        public byte[]? ScreenShot { get; set; }
    }

    [DataContract]
    public class ProjectDataList
    {
        [DataMember]
        public List<ProjectData> projectsData { get; set; }
    }

    /*OpenProject作为OpenProjectControl的DataContext出现，但是OpenProjectControl中只有一个用于选择打开项目的ListBox，其中的内容并不会被改变，
    这个部分的属性性不需要INotifyPropertyChanged的OnProperryChange函数的调用，只需要获得项目列表属性即可，故，这里并不需要继承自ViewModelBase*/
    // 注意如果一个类要作为一个user control的data context，它不能是static class
    internal class OpenProject
    {
        static private readonly string _projectsDataDirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PrimalEditor");
        static private string _projectsDataPath = string.Empty; 

        static private ObservableCollection<ProjectData> _projects = new ObservableCollection<ProjectData>();
        static public ReadOnlyObservableCollection<ProjectData> Projects
        {
            get;
        }

        /*从recentProjectsData.xml读取项目信息，保存到属性‘Projects’中*/
        static private void ReadRecentProjectsData()
        {
            if (!File.Exists(_projectsDataPath))
                return;
            try
            {
                List<ProjectData>? projectsData = Utilities.Serializier.FromFile<ProjectDataList>(_projectsDataPath)?.projectsData.OrderByDescending(x=>x.LastOpenTime).ToList();
                if (projectsData != null)
                {
                    _projects.Clear();
                    foreach (var projectData in projectsData)
                    {
                        // 检测到项目被删除，即有可能某个上次打开过的项目被删除了，不显示这个项目了
                        if (File.Exists(projectData.FilePath))
                        {
                            projectData.Icon = File.ReadAllBytes(Path.Combine(Path.Combine(projectData.ProjectPath ?? "", ".Primal"), "icon.png"));
                            projectData.ScreenShot = File.ReadAllBytes(Path.Combine(Path.Combine(projectData.ProjectPath ?? "", ".Primal"), "screenShot.png"));
                            _projects.Add(projectData);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.Write(ex.Message);
                Logger.Log($"Failed to read recent projects in {_projectsDataPath}.Error Message:\n" + ex.Message, MessageType.Error);
                throw;
            }

        }

       /*将属性‘Projects’中的内容保存到recentProjectsData.xml文件中*/
        static private void WriteRecentProjectsData()
        {
            try
            {
                // 写入的时候按照“从老到新”的顺序
                Utilities.Serializier.ToFile(new ProjectDataList() { projectsData = _projects.OrderBy(x => x.LastOpenTime).ToList() }, _projectsDataPath);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                Logger.Log($"Failed to write recent projects in {_projectsDataPath}.Error Message:\n" + ex.Message, MessageType.Error);
                throw;
            }
        }

        static public Project? Open(ProjectData projectData)
        {
            // 首先要重新读一遍项目数据，虽然构造函数中读取了一次项目数据，但是，当我们在这个应用程序中再次打开一个项目时，项目数据可能发生改变了，因为其他PrimalEditor也可能写入了数据
            // 说白了就是，recentProjectsData.xml文件可能在启动程序后到用户打开一个项目这期间，发生了改变
            ReadRecentProjectsData();
            
            /*查一下这个项目在不在最近打开过的项目文件中，如果在则更新信息，不在则添加该项目，然后更新recentProjectsData.xml文件**/
            ProjectData? curProject = _projects.FirstOrDefault(x => projectData.FilePath == x.FilePath);
            if(curProject == null)
            {
                projectData.LastOpenTime = DateTime.Now;
                _projects.Add(projectData);
                curProject = projectData;
            }
            else
            {
                curProject.LastOpenTime = DateTime.Now;
            }
            WriteRecentProjectsData();

            return Project.Load(curProject.FilePath);
        }

        static OpenProject()
        {
            Projects = new ReadOnlyObservableCollection<ProjectData>(_projects);
            /*recentProjectsData.xml文件，这里面有保存最近打开过的项目文件信息*/
            try
            {
                // 创建recentProjectsData.xml文件的目录
                if (!Directory.Exists(_projectsDataDirPath))
                {
                    Directory.CreateDirectory(_projectsDataDirPath);
                }
                _projectsDataPath = Path.Combine(_projectsDataDirPath, "recentProjectsData.xml");

                ReadRecentProjectsData();
            }
            catch(Exception ex) 
            {
                Debug.Write(ex.Message);
                Logger.Log($"Failed to read recent projects in {_projectsDataPath}.Error Message:\n" + ex.Message, MessageType.Error);
                throw;
            }
        }
    }
}
