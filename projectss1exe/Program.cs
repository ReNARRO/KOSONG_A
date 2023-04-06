using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace projectss1exe
{
    class Program
    {
        static void Main(string[] args)
        {
            string jwb;
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke Database\n");
                    Console.WriteLine("Masukkan User ID: ");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password: ");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan Database tujuan: ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = LAPTOP-N8UQTM32\\RENARRO_23; " +
                                    "initial catalog = {0}; " +
                                    "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat seluruh data");
                                        Console.WriteLine("2. Tambah data");
                                        Console.WriteLine("3. Keluar");
                                        Console.WriteLine("4. Hapus data");
                                        Console.WriteLine("\nEnter your choice (1-4): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Data Pengepul\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Input Data Pengepul\n");
                                                    Console.WriteLine("Masukkan Id Pengepul :");
                                                    string id = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Pengepul :");
                                                    string NmaPe = Console.ReadLine();
                                                    Console.WriteLine("Masukkan ALamat Jalan :");
                                                    string Jalan = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Alamat Kecamatan :");
                                                    string kec = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Alamat Kota :");
                                                    string kot = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Telepon :");
                                                    string Nohp = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Tanggal Penerimaan :");
                                                    string tgl = Console.ReadLine();
                                                    if (id.Equals(pr.searchdata(id, conn)))
                                                    {
                                                        Console.WriteLine("data sudah ada");
                                                    }
                                                    else
                                                    {
                                                        try
                                                        {
                                                            pr.insert(id, NmaPe, Jalan, kec, kot, Nohp, tgl, conn);

                                                        }
                                                        catch
                                                        {
                                                            Console.WriteLine("\nAnda tidak memiliki akses untuk menambah data ");
                                                        }
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            case '4':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Hapus Data Mahasiswa");
                                                    Console.WriteLine("Masukan NIM: ");
                                                    string id = Console.ReadLine();
                                                    Console.WriteLine("Apakah anda yakin ingin menghapus pengepul ini?(y)");
                                                    jwb = Console.ReadLine();

                                                    if (jwb.Equals("y"))
                                                    {
                                                        try
                                                        {
                                                            pr.delete(id, conn);
                                                        }
                                                        catch
                                                        {
                                                            Console.WriteLine("Anda tidak memiliki akses untuk menghapus data");
                                                        }
                                                    }
                                                    else break;
                                                }
                                                break;
                                            case '5':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Input Data Pengepul\n");
                                                    Console.WriteLine("Masukkan Id Pengepul :");
                                                    string id = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Pengepul :");
                                                    string NmaPe = Console.ReadLine();
                                                    Console.WriteLine("Masukkan ALamat Jalan :");
                                                    string Jalan = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Alamat Kecamatan :");
                                                    string kec = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Alamat Kota :");
                                                    string kot = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Telepon :");
                                                    string Nohp = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Tanggal Penerimaan :");
                                                    string tgl = Console.ReadLine();

                                                    pr.update(id, NmaPe, Jalan, kec, kot, Nohp, tgl, conn);
                                                }
                                                break;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak dapat mengakses database menggunakan user tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select*From Pengepul", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();

            }
            r.Close();
        }

        public void insert(string id, string NmaPe, string Jalan, string kec, string kot, string Nohp, string tgl, SqlConnection con)
        {
            string str = "";
            str = "insert into Pengepul (Id_Pengepul, Nama_pengepul,Jalan, Kecamatan, Kota, No_HP, Tgl_Penerimaan ) values(@id, @nma, @jln, @kec, @kt, @phn, @tgl)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id", id));
            cmd.Parameters.Add(new SqlParameter("nma", NmaPe));
            cmd.Parameters.Add(new SqlParameter("jln", Jalan));
            cmd.Parameters.Add(new SqlParameter("kec", kec));
            cmd.Parameters.Add(new SqlParameter("kt", kot));
            cmd.Parameters.Add(new SqlParameter("phn", Nohp));
            cmd.Parameters.Add(new SqlParameter("tgl", tgl));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
        public void delete(string id, SqlConnection con)
        {
            string str = "";
            str = "delete from Pengepul where Id_Pengepul = @id";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id", id));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Dihapus");
        }

        public string searchdata(string id, SqlConnection con)
        {
            string str = "";
            string nim = "";
            str = "select * from Pengepul where Id_Pengepul = @id ";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id", id));
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                nim = r.GetValue(0).ToString();
                Console.WriteLine();
            }
            r.Close();
            return nim;
        }

        public void update(string id, string NmaPe, string Jalan, string kec, string kot, string Nohp, string tgl, SqlConnection con)
        {
            string str = "";
            str = "Update Pengepul set Nama_Pengepul = @nma,Jalan = @jln,Kecamatan = @kec, Kota = @kt, No_HP = @phn, Tgl_Penerimaan = @tgl  where Id_Pengepul = @id";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id", id));
            cmd.Parameters.Add(new SqlParameter("nma", NmaPe));
            cmd.Parameters.Add(new SqlParameter("jln", Jalan));
            cmd.Parameters.Add(new SqlParameter("kec", kec));
            cmd.Parameters.Add(new SqlParameter("kt", kot));
            cmd.Parameters.Add(new SqlParameter("Phn", Nohp));
            cmd.Parameters.Add(new SqlParameter("tgl", tgl));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil ditambahkan");

        }


    }
}
