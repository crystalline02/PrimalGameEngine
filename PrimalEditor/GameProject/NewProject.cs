using PrimalEditor.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.GameProject
{
    [DataContract]
    internal class ProjectTemplate
    {
        [DataMember]
        public string? ProjectType { get; set; }
        [DataMember]
        public string? ProjectFile { get; set; }
        [DataMember]
        public List<string>? Folders { get; set; }

        //注意一下，只有C#中的属性才能在xmal文件中被Bind
        public string? IconPath { get; set; }
        public string? ScreenShotPath { get; set; }
        public byte[]? Icon { get; set; }
        public byte[]? ScreenShot { get; set; }
        public string? ProjectFilePath { get; set; }
    }

    internal class NewProject: ViewModelBase
    {
        // TO DO: Get path from primal edtor installation location
        private readonly string _templatesPath = @"..\..\PrimalEditor\ProjectTemplates";
        private ObservableCollection<ProjectTemplate> _projectTemplates = new ObservableCollection<ProjectTemplate>();
        public ReadOnlyObservableCollection<ProjectTemplate> ProjectTemplates
        {
            get;
        }

        private string _errorMsg;
        public string ErrorMsg
        {
            get => _errorMsg;
            set
            {
                if(_errorMsg != value)
                {
                    _errorMsg = value;
                    OnPropertyChanged(nameof(ErrorMsg));
                }
            }
         }

        private bool _isValid;
        public bool IsValid
        {
            get => _isValid;
            set
            {
                if(_isValid != value)
                {
                    _isValid = value;
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        private string _projectName = "NewProject";
        public string ProjectName
        {
            get => _projectName;
            set
            {
                if(_projectName != value)
                {
                    _projectName = value;
                    ValidateProjectPath();
                    OnPropertyChanged(nameof(ProjectName));
                }
            }
        }

        private string _projectPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\PrimalEngine\";
        public string ProjectPath
        {
            get => _projectPath;
            set
            {
                if (_projectPath != value)
                {
                    _projectPath = value;
                    ValidateProjectPath();
                    OnPropertyChanged(nameof(ProjectPath));
                }
            }
        }

        public string GetFullProjectPath()
        {
            string fullProjectPath = ProjectPath;
            if (!Path.EndsInDirectorySeparator(fullProjectPath))
            {
                fullProjectPath += Path.DirectorySeparatorChar;
            }
            fullProjectPath += ProjectName;
            return fullProjectPath;
        }

        private bool ValidateProjectPath()
        {
           string projectPath = GetFullProjectPath();

            IsValid = false;
            // 检查ProjectName合不合法，检查ProjectPath合不合法
            if(string.IsNullOrWhiteSpace(ProjectName.Trim()))
            {
                ErrorMsg = "Empty project name";
            }
            else if(ProjectName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                ErrorMsg = "Invaild character(s) in project name";
            }
            else if(string.IsNullOrWhiteSpace(ProjectPath.Trim()))
            {
                ErrorMsg = "Empty project path";
            }
            else if(ProjectPath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                ErrorMsg = "Invaikd character(s) in project path";
            }
            else if(Directory.Exists(projectPath) && Directory.EnumerateFileSystemEntries(projectPath).Any())
            {
                ErrorMsg = "Project file is not empty";
            }
            else
            {
                IsValid = true;
                ErrorMsg = "";
            }

            return IsValid;
        }

        public string CreateNewProject(ProjectTemplate? template)
        {
            if(template == null)
                return string.Empty;
            ValidateProjectPath();

            string projectPath = GetFullProjectPath();

            try
            {
                // 创建项目目录
                if(!Directory.Exists(projectPath))
                {
                    Directory.CreateDirectory(projectPath);
                }

                // 创建项目目录中的所有文件
                if(template.Folders != null)
                {
                    foreach(string fileName in template.Folders)
                    {
                        Directory.CreateDirectory(Path.GetFullPath(Path.Combine(projectPath, fileName)));
                    }
                }

                // 特别地，设置.primal文件夹的属性
                DirectoryInfo dotPrimalDir = new DirectoryInfo((Path.GetFullPath(Path.Combine(projectPath, ".Primal"))));
                dotPrimalDir.Attributes |= FileAttributes.Hidden;

                // 拷贝icon和screenShot到.primal文件夹
                if(template.IconPath != null)
                    File.Copy(template.IconPath, Path.GetFullPath(Path.Combine(dotPrimalDir.FullName, "icon.png")));
                if(template.ScreenShotPath != null)
                    File.Copy(template.ScreenShotPath, Path.GetFullPath(Path.Combine(dotPrimalDir.FullName, "screenShot.png")));

                // 在项目目录写入项目文件{ProjectName}{Project.Extention}，只需要从模板目录拷贝project.primal稍做修改即可
                if (template.ProjectFilePath != null)
                {
                    string templateProjectXML = File.ReadAllText(template.ProjectFilePath);
                    string newProjectXML = string.Format(templateProjectXML, ProjectName, projectPath);
                    File.WriteAllText(Path.GetFullPath(Path.Combine(projectPath, $"{ProjectName}{Project.Extention}")), newProjectXML);
                }

                return projectPath;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Logger.Log("Failed to create a new project.Error Message:\n" + e.Message, MessageType.Error);
                throw;
            }
        }

        public NewProject()
        {
            _errorMsg = string.Empty;
            // 这里将_projectTemplates包装为ProjectTemplates（只读属性），这样只要更新_projectTemplates，那么属性ProjectTemplates就被更新
            ProjectTemplates = new ReadOnlyObservableCollection<ProjectTemplate>(_projectTemplates);
            try
            {
                /*Read project templates and show them in the new project controler*/

                string[] allTemplatesXML = Directory.GetFiles(_templatesPath, "template.xml", SearchOption.AllDirectories);
                
                Debug.Assert(allTemplatesXML.Any());
                foreach (string template in allTemplatesXML)
                {
                    ProjectTemplate? pt = Utilities.Serializier.FromFile<ProjectTemplate>(template);
                    if (pt != null)
                    {
                        string projectPath = Path.GetDirectoryName(template) ?? @"./";
                        pt.IconPath = Path.GetFullPath(Path.Combine(projectPath, "icon.png"));
                        pt.Icon = File.ReadAllBytes(pt.IconPath);
                        pt.ScreenShotPath = Path.GetFullPath(Path.Combine(projectPath, "screenShot.png"));
                        pt.ScreenShot = File.ReadAllBytes(pt.ScreenShotPath);
                        pt.ProjectFilePath = Path.GetFullPath(Path.Combine(projectPath, pt.ProjectFile ?? "")); 

                        _projectTemplates.Add(pt);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Logger.Log("Failed to read and init project templates.Error Message:\n" + e.Message, MessageType.Error);
                throw;
            }
            ValidateProjectPath();
        }
    }
}
