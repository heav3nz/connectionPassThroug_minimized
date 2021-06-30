using EmployeesService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesService.Data.Services
{
    public class EmployeeService
    {

        private DbContext _Database = null;
        public EmployeeService()
        {
            _Database = new DbContext();
        }

        public List<EmployeeViewModel> GetEmployesJson()
        {
            //se recomienda encapsular las consultas en procedimientos, para mejorar la seguridad, por ejemplo, no mostrar los objetos que existen en la BD
            return _Database.SqlQuery<EmployeeViewModel>("SELECT numero_empleado as numeroEmpleado, nombres, apellidos  FROM dbo.empleados;");
        }


        //Destructor
        ~EmployeeService()
        {
            _Database = null;
        }

    }
}
