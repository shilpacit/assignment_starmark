using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnectiion
{

    class Patients
    {
        public int pid { get; set; }
        public string pname { get; set; }

        public string paddress { get; set; }

        public int did { get; set; }


    }

    class Docter
    {
        public int did { get; set; }
        public string dname { get; set; }
    }

    class medicaldisconnected
    {
        static string Con = "Data Source=192.168.171.36;Initial Catalog=3332;Integrated Security=True";
        static string query = "select * from patient; select * from docter";
        static DataSet ds = new DataSet();

        static SqlDataAdapter ada = null;


        static void fillrecord()
        {
            SqlConnection sql = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand(query, sql);
            ada = new SqlDataAdapter(cmd);

            SqlCommandBuilder builder = new SqlCommandBuilder(ada);
            ada.Fill(ds);
            ds.Tables[0].TableName = "patientlist";

            //to set primary key
            if (ds.Tables[0].PrimaryKey.Length == 0)
            {
                ds.Tables[0].PrimaryKey = new DataColumn[]
                {
                    ds.Tables[0].Columns[0]

                };



            }     ds.Tables[1].TableName = "docterlist";

        }

        static void insertpatient(Patients patient)
        {
            DataRow newRow = ds.Tables[0].NewRow();
            int id = 0;
            newRow[0] = patient.pid;
            newRow[1] = patient.pname;
            newRow[2] = patient.paddress;
            newRow[3] = patient.did;


            ds.Tables[0].Rows.Add(newRow);
            ada.Update(ds, "patientlist");

        }
        public static void adddocter(Docter docter)
        {
            DataRow newRow = ds.Tables[0].NewRow();
            newRow[0] = docter.did;
            newRow[1] = docter.dname;

            ds.Tables[0].Rows.Add(newRow);
            ada.Update(ds, "docterlist");

        }
        public static void updatepatient(Patients p)
        {
            var selectedrow = ds.Tables[0].Rows.Find(p.pid);
            selectedrow[1] = p.pname;
            selectedrow[2] = p.paddress;
            selectedrow[3] = p.did;

            ada.Update(ds, "patientlist");

        }

        static void deletepatient( int id)
        {
            var selected = ds.Tables[0].Rows.Find(id);
            if(selected !=null)
            {
                selected.Delete();

            }
            ada.Update(ds, "patientlist");
        }
       static  List<Patients> getallrecords()
        {

            List<Patients> patienss = new List<Patients>();
            foreach (DataRow row in ds.Tables["patientlist"].Rows)
            {
                Patients p = new Patients { pid = (int)row[0], pname = row[1].ToString(), paddress = row[2].ToString(), did = (int)row[3] };
                patienss.Add(p);

            }
            return patienss;
        }

         

        

 public static void insertpatientHelper()
        {
            Console.WriteLine("enter the patient id");
            int Paid = int.Parse(Console.ReadLine());

            Console.WriteLine("enter the pname");
            string name = Console.ReadLine();
            Console.WriteLine(" enter the paddress");
            string address = Console.ReadLine();
            Console.WriteLine("enter the did");
            int ddid = int.Parse(Console.ReadLine());
            Patients patient = new Patients {pid=Paid, pname = name, paddress = address, did = ddid };
            insertpatient(patient);
            Console.WriteLine("ADDED SUCESSFULLY");


        }
      
        public static void  inserdocterhelper()
            {
            Console.WriteLine("enter the docter id");
            int ddid = int.Parse(Console.ReadLine());

            Console.WriteLine("enter the docter name");
            string name = Console.ReadLine();

            Docter docter = new Docter { did = ddid, dname = name };

            adddocter(docter);
        }
        public static void updatepatienthelper()
        {
            Console.WriteLine("enter the id u want to update");
            int uid = int.Parse(Console.ReadLine());
            Console.WriteLine("enter the pname");
            string uname = Console.ReadLine();
            Console.WriteLine(" enter the paddress");
            string uaddress = Console.ReadLine();
            Console.WriteLine("enter the did");
            int uddid = int.Parse(Console.ReadLine());
            Patients p = new Patients { pid = uid, pname = uname, paddress = uaddress, did = uddid };
            updatepatient(p);
        }

       static void  deletepatienthelper()
        {
            Console.WriteLine("enter the id u want to delete");
            int deletedid = int.Parse(Console.ReadLine());
            deletepatient(deletedid);
        }


    static void  Getallrecordhelper()
        {
            List<Patients> patiens = getallrecords();

            foreach (var item in patiens)

            {
                Console.WriteLine(item.pid + " " + item.pname + " " + item.paddress + " " + item.did);
            }
        }

        public static void Main(string[] args)
        {

            bool processing = true;
            fillrecord();
            do
            {
                string menu = "1.addpatient\n2.adddocters\n3.updatepatient\n4.deletepatient\n5.getAllPatientRecords 6: press any key to exit";
                Console.WriteLine(menu);
                Console.WriteLine("enter the choice");
             
                
                int choice = int.Parse(Console.ReadLine());
             
                processing = processmenu(choice);



            } while (processing);
        }
            public static bool processmenu( int choice)
            {
                switch (choice)
                {
                    case 1:
                        insertpatientHelper();
                        break;
                    case 2:
                        inserdocterhelper();
                        break;

                    case 3:
                        updatepatienthelper();
                        break;
                    case 4:
                        deletepatienthelper();
                        break;
                    case 5:
                        Getallrecordhelper();
                        break;
                default:
                    return false;

                }
            return true;
            }
           
        }
    }


