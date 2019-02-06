using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerialPortApp.Model;

namespace SerialPortApp.ViewModel
{
    public class StudentViewModel { 
	
        private Student _selectedStudent;
 
        public Student SelectedStudent { 
            get { 
                return _selectedStudent; 
            } 
	
            set { 
                _selectedStudent = value;
                DeleteCommand.RaiseCanExecuteChanged(); 
            } 
        }
        public MyICommand DeleteCommand { get; set;} 

        public StudentViewModel() { 
            LoadStudents(); 
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
        }
        public ObservableCollection<Student> Students { 
            get; 
            set; 
        }
		
        private void OnDelete() { 
            Students.Remove(SelectedStudent); 
        }

        private bool CanDelete() { 
            return SelectedStudent != null; 
        }
        public void LoadStudents() { 
            ObservableCollection<Student> students = new ObservableCollection<Student>();
				
            students.Add(new Student { FirstName = "Mark", LastName = "Allain" }); 
            students.Add(new Student { FirstName = "Allen", LastName = "Brown" }); 
            students.Add(new Student { FirstName = "Linda", LastName = "Hamerski" }); 
			
            Students = students; 
        } 
    } 
}
