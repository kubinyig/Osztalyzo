using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Osztalyzo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public List<Student> students = new List<Student>();
    public MainWindow()
    {
        InitializeComponent();
        ShowStudents();
        AddStudentButton.Click += (s, e) => Addstudent();
    }
    void SaveStudents()
    {
        using StreamWriter sw = new StreamWriter("students.txt");
        foreach (Student student in students)
        {
            sw.Write(student.name + ":");
            for (int i = 0; i < student.grades.Count; i++)
            {
                sw.Write(student.grades[i]);
                if (i < student.grades.Count - 1)
                    sw.Write(",");
            }
            sw.WriteLine();
        }
    }
    void loadStudents()
    {
        using StreamReader sr = new StreamReader("students.txt");
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(':');
            Student student = new Student(parts[0]);
            if (parts.Length > 1)
            {
                string[] grades = parts[1].Split(',');
                foreach (string grade in grades)
                {
                    student.grades.Add(int.Parse(grade));
                }
            }
            students.Add(student);
        }
        ShowStudents();
    }
    void Addstudent()
    {
        if (students.Where(s => s.name == NameTextBox.Text).Count() == 0)
        {
            Student student = new Student(NameTextBox.Text);
            students.Add(student);
            ShowStudents();
            NameTextBox.Clear();
            GradeTextBox.Clear();
        }
        else
        {
            Student student = students.Where(s => s.name == NameTextBox.Text).First();
            student.grades.Add(int.Parse(GradeTextBox.Text));
            ShowStudents();
            GradeTextBox.Clear();
            NameTextBox.Clear();
        }

    }
    void ShowStudents()
    {
        GradesStackPanel.Children.Clear();
        foreach (Student student in students)
        {
            Grid onestudentgrid = new Grid();
            var col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            var col2 = new ColumnDefinition();
            col.Width = new GridLength(5, GridUnitType.Star);
            var col3 = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            onestudentgrid.ColumnDefinitions.Add(col);
            Label nameLabel = new Label();
            nameLabel.Content = student.name;
            StackPanel gradesPanel = new StackPanel();
            gradesPanel.Orientation = Orientation.Horizontal;
            gradesPanel.MinWidth = 30;
            foreach (var grade in student.grades)
            {
                Label gradeLabel = new Label();
                gradeLabel.Content = grade.ToString();
                gradesPanel.Children.Add(gradeLabel);
            }
            Label averageLabel = new Label();
            averageLabel.Content = Math.Round(student.Average(),2).ToString();
            Grid.SetColumn(gradesPanel, 1);
            Grid.SetColumn(averageLabel, 2);
            onestudentgrid.Children.Add(nameLabel);
            onestudentgrid.Children.Add(gradesPanel);
            onestudentgrid.Children.Add(averageLabel);
            GradesStackPanel.Children.Add(onestudentgrid);
        }
    }

}