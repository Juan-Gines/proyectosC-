using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;


namespace CRUD_Linq
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClasses1DataContext dataContext;

        public MainWindow()
        {
            InitializeComponent();

            string miConexionSql = ConfigurationManager.ConnectionStrings["CRUD_Linq.Properties.Settings.CrudLinqSql"].ConnectionString;

            dataContext = new DataClasses1DataContext(miConexionSql);

            
        }

        public void InsertarEmpresa()
        {
            Empresa Tecsymold = new Empresa { Nombre = "Tecsymold" };

            dataContext.Empresa.InsertOnSubmit(Tecsymold);

            Empresa PildorasInformaticas = new Empresa { Nombre = "Píldoras Informáticas" };

            dataContext.Empresa.InsertOnSubmit(PildorasInformaticas);

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empresa;

        }

        public void InsertarEmpleado()
        {
            Empresa Tecsymold = dataContext.Empresa.First(em => em.Nombre.Equals("Tecsymold"));

            Empresa PildorasInformaticas = dataContext.Empresa.First(em => em.Nombre.Equals("Píldoras Informáticas"));

            List<Empleado> listaEmpleados = new List<Empleado>();

            listaEmpleados.Add(new Empleado { Nombre = "Juan Ginés", Apellido = "Alentà", EmpresaId = Tecsymold.Id });

            listaEmpleados.Add(new Empleado { Nombre = "Juan", Apellido = "Díaz", EmpresaId = PildorasInformaticas.Id });

            listaEmpleados.Add(new Empleado { Nombre = "Lidia", Apellido = "Lozano", EmpresaId = Tecsymold.Id });

            listaEmpleados.Add(new Empleado { Nombre = "Raquel", Apellido = "Rascón", EmpresaId = PildorasInformaticas.Id });

            dataContext.Empleado.InsertAllOnSubmit(listaEmpleados);

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empleado;
        }
    }
}
