using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//ADDED THIS
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InventoryManagementSystemSQLite
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //ADDED THIS
            //this ensures that the database file is created if it doesnt exist
            DatabaseFacade facade = new DatabaseFacade(new DataContext());
            facade.EnsureCreated();


            Application.Run(new Form1());

        }
      
    }
}
