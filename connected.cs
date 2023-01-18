using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sampleconapp
{
    class Patient
    {
        public string  pname { get; set; }
        public string paddress { get; set; }
        public int did { get; set; }
        public int pid { get; set; }


    }
    class docters
    {
        public int name { get; set; }
    }

    class hospital
    {
       static  string Con = "Data Source=192.168.171.36;Initial Catalog=3332;Integrated Security=True";

     static   string insert = "insert into patient values(@pname,@paddress,@Did)";
        static string update = "update patient set pname=@pname,paddress=@paddress,Did=@Did where pid=@pid";
        static string delete = "delete from patient where pid=@pid";

        static string display="select * from patient";
 
       static  void insertrecord(Patient patient)
        {
            SqlConnection sql = new SqlConnection(Con);
            SqlCommand command = new SqlCommand(insert, sql);
            command.Parameters.AddWithValue("@pname", patient.pname);
            command.Parameters.AddWithValue("@paddress", patient.paddress);
            command.Parameters.AddWithValue("@Did", patient.did);

            try
            {
                sql.Open();
                command.ExecuteNonQuery();
                
            }
            catch(Exception Ex)
            {
             Console.WriteLine( Ex.Message);
            }
            finally
            {
                sql.Close();

            }

        }
        static  void Update( Patient patient)
        {
            SqlConnection sql = new SqlConnection(Con);
            SqlCommand command = new SqlCommand(update, sql);
            command.Parameters.AddWithValue("@pname", patient.pname);
            command.Parameters.AddWithValue("@paddress", patient.paddress);
            command.Parameters.AddWithValue("@Did", patient.did);
            command.Parameters.AddWithValue("@pid", patient.pid);

            try
            {
                sql.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            finally
            {
                sql.Close();

            }
        }
      static void   Delete(int id)
        {
            SqlConnection sql = new SqlConnection(Con);
            SqlCommand command = new SqlCommand(delete, sql);
            command.Parameters.AddWithValue("@pid", id);
            try
            {
                sql.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            finally
            {
                sql.Close();

            }

        }

       static  List<Patient> Display()
        {
            SqlConnection sql = new SqlConnection(Con);
            SqlCommand command = new SqlCommand(display, sql);
            List<Patient> patiens = new List<Patient>();


            try
            {
                sql.Open();
                var items = command.ExecuteReader();

                while(items.Read())
                {
                    Patient patient = new Patient { pid = (int)items[0], pname=items[1].ToString(), paddress = items[2].ToString(), did = (int)items[3] };
                    
                    patiens.Add(patient);
                }


            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            finally
            {
                sql.Close();

            }
            return patiens;

        }
    
       static  void inserthelper()
        {
            Console.WriteLine("enter the patient name");
            string name = Console.ReadLine();
            Console.WriteLine(" enter the paddress");
            string address = Console.ReadLine();
            Console.WriteLine("enter the did");
            int ddid = int.Parse(Console.ReadLine());

            Patient patient = new Patient { pname = name, paddress = address, did = ddid };

            insertrecord(patient);

                
        }
        static void Updatehelper()
        {
            Console.WriteLine("enter the pid");
                int id = int.Parse(Console.ReadLine());

            Console.WriteLine("enter the patient name");
            string name = Console.ReadLine();
            Console.WriteLine(" enter the paddress");
            string address = Console.ReadLine();
            Console.WriteLine("enter the did");
            int ddid = int.Parse(Console.ReadLine());
            Patient patient = new Patient {pid=id ,pname = name, paddress = address, did = ddid };
            Update(patient);

        }
        static void deletehelper()
        {
            Console.WriteLine("enter the pid");
            int id = int.Parse(Console.ReadLine());

            Delete(id);


        }
     static    void  Displayhelper()
            {
            List<Patient> obj = Display();
            foreach (var item in obj)
            {
                Console.WriteLine(item.pid + " " + item.pname + " " + item.paddress + " " + item.did);
            }

        }


            static void Main(string[] args)
        {
            // hospital.inserthelper();
            // updatehelper();
            //  deletehelper();
            Displayhelper();
        }

    }


}
